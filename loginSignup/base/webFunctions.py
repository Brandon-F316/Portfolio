import fitz  
import re
import os
from docx2pdf import convert
import aspose.words as aw
import ollama
import requests
import pandas as pd

def extract_outcomes(text):
    # Construct a prompt asking Ollama to extract a specific section
    print("beginning extraction")
    prompt = f"""
    You are a helpful assistant that specilizes in extracting bulleted points from syllabus . 
    You task is to identify the points under heading Objectives and student learning outcomes. The bullet points are in the format of having numbers in braces at the end with comma sepated numbers.Extract all the bullet points as it is.
    Donot change anything in the text. I dont want summaries. Just the points as they are. 
    Reply in markdown. No extra text . Justbullet points.
    Here is the text:  \n\n{text}"""

    headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer <your_ollama_api_key>'  
    }

    data = {
        'model': 'llama3.2',  # model
        'prompt': prompt,
        'max_tokens': 500  
    }

    response = requests.post("http://localhost:11434/v1/completions", headers=headers, json=data)

    # Check if the request was successful
    if response.status_code == 200:
        result = response.json()
        return result.get("choices", [])[0].get("text", "No text extracted.")
    else:
        return f"Error: {response.status_code} - {response.text}"
    

# docx to pdf handling
def doc_to_pdf(file_path, output_path):
    doc = aw.Document(file_path)
    doc.save(output_path)


#take PDF and extract the text
def file_to_txt(file_path):
    text = ""
    with fitz.open(file_path) as pdf:
        for page in pdf:
            text += page.get_text()

    return text

#clean up the data from the PDF
def clean_text(text):
    text = re.sub(r'\n+', ' ', text)  # Replace newlines with spaces
    text = re.sub(r'\s+', ' ', text)  # Normalize spaces
    
    return text

def extract_PI_section(text):
    pattern = r"([A-Za-z , )]+)\s\([\d,]*\)"
    matches = re.findall(pattern, text)
    performance_indicators = []
    for match in matches:
        cleaned_description = match.strip("(").strip()

        # # Only add the PI if the description is less than 300 characters
        # if len(cleaned_description) >= 300:
        #     continue  # Skip the PI if its description is too long



        # Append the PI to the list
        performance_indicators.append(cleaned_description)
    
    return performance_indicators




def process_pdf(file_path):
    """Process a PDF to extract performance indicators."""
    # Extract and clean text
    raw_text = file_to_txt(file_path)
    cleaned_text = clean_text(raw_text)

    # Extract performance indicators
    return extract_PI_section(cleaned_text)


def extract_grades(excel_file):
    df = pd.read_excel(excel_file)

    output = ""
    section_number = 1

    for col in df.columns:
        if 'Grades' in col:
            grades = df[col].dropna().astype(str).tolist()
            grades_string = ", ".join(grades)

            if grades_string:
                output += f"Grades section {section_number}\n{grades_string}\n//\n"
                section_number += 1

    print("DEBUG extracted output:\n", output)

    return output
from django.shortcuts import render, redirect, get_object_or_404
from django.http import JsonResponse


from django.contrib.auth.decorators import login_required

from django.http import JsonResponse

from django.contrib import messages
from django.contrib.auth import authenticate, login, logout, update_session_auth_hash
from .forms import CreateUserForm, UploadFileForm, UploadedFileForm, AssessForm, DataForm, CourseForm, UpdateUserForm
from .models import UploadedFile, UploadedData, Course, CustomUser

from .webFunctions import file_to_txt, extract_PI_section, clean_text, doc_to_pdf, extract_outcomes, extract_grades

# Create your views here.

@login_required
def home(request):
    user=request.user

    if(user.is_superuser):
        return redirect("/admin")
    
    return render(request, "pages/professor/assessOption.html", {})


def authView(request):
    if request.user.is_authenticated:
        return redirect("home")
    
    if request.method == "POST":
        form = CreateUserForm(request.POST or None)
        if form.is_valid():
            form.save()
            user = form.cleaned_data.get("username")
            messages.success(request, "Account was Created for " + user)

            return redirect("base:login")     
        else:
            messages.error(request, "There was an error with your registration. Please check the form and try again." )
    else:
        form = CreateUserForm()
    return render(request, "registration/signup.html", {"form" :form})


@login_required
def assessOption(request):
    return render(request, "pages/professor/assessOption.html", {})


@login_required
def ManAssess(request):
    if request.method == "POST":
        combined_text = request.POST.get("combined_text", "")

        uploaded_info = UploadedData.objects.create(outcomes=combined_text, user=request.user)
        request.session['uploadedData_id'] = uploaded_info.data_id  
        return redirect('/Mquestions')

    return render(request, "pages/professor/ManAssess.html", {})

@login_required
def ManQuestions(request):
    info_id = request.session.get('uploadedData_id')

    if request.method == "POST":
        combined_text = request.POST.get("combined_text", "")

        uploaded_info = UploadedData.objects.get(data_id = info_id) 
        uploaded_info.questions = combined_text
        uploaded_info.save()
        request.session['uploadedData_id'] = uploaded_info.data_id

        return redirect('/Mgrades')

    return render(request, "pages/professor/ManQuestions.html", {})


@login_required
def Grades(request):
    info_id = request.session.get('uploadedData_id')

    if request.method == "POST":
        uploaded_info = UploadedData.objects.get(data_id = info_id)
        excel_file = request.FILES['excel_file']
        extracted_content = extract_grades(excel_file)

        uploaded_info.grades = extracted_content
        uploaded_info.save()
        request.session['uploadedData_id'] = uploaded_info.data_id

        return redirect("/target")


    return render(request, "pages/professor/grades.html", {})

@login_required
def targetScores(request):



    return render(request, "pages/professor/targetScore.html", {})



@login_required
def Questions(request):
    form = DataForm()
    file_id = request.session.get('uploadedData_id')
    if not file_id:
        return redirect('/test')
    if request.method == "POST":

        if form.is_valid():
            form.save()
        

    uploaded_file = get_object_or_404(UploadedData, data_id=file_id, user=request.user)

    return render(request, "pages/professor/questions.html", {"form": form,"uploaded_file": uploaded_file})


from django.shortcuts import redirect, render
from django.contrib.auth.decorators import login_required

@login_required
def Courses(request):
    user = request.user

    unassigned = Course.objects.filter(professor__isnull=True, program=user.program_field)
    my_courses = Course.objects.filter(professor=user)

    if request.method == 'POST':
        course = request.POST['course_id']
        semester = request.POST['semester']
        year_int = int(request.POST['year'])

        if Course.objects.filter(course_id=course).exists():
            messages.error(request, "That course is already taken")
        else:
            course = Course.objects.create(course_id=course, year=year_int,semester=semester,program=user.program_field, professor=user)
        return redirect('/courses')

    form = CourseForm()
    return render(request, "pages/professor/courses.html", {"form":form, "courses":unassigned, "my_courses":  my_courses,})


@login_required
def ViewData(request):
    return render(request, "pages/professor/data.html", {})

@login_required
def UserSettings(request):
    if request.method == "POST":
        form = UpdateUserForm(request.POST, instance=request.user)
        if form.is_valid():
            user = form.save(commit=False)

            password1 = form.cleaned_data.get('password1')
            if password1:
                user.set_password(password1)
                update_session_auth_hash(request, user)

            user.save()
            messages.success(request, "✅ Your account was updated successfully!")
            return redirect('/settings')
        else:
            print(form.errors)
    else:
        form = UpdateUserForm(instance=request.user)

    context = {'form': form}
    return render(request, "pages/professor/settings.html", context)

@login_required
def profile(request):
    form = CreateUserForm(request.POST or None)
    
    context = {
        'form':form,
        'access_field': request.user.access_field,
        'program_field': request.user.program_field,
    }
    
    return render(request, "pages/professor/profile.html", context)



def logoutUser(request):
    logout(request)
    return redirect("/accounts/logout")




#unused
@login_required
def LLm_test(request):
    if request.method == "POST":
        form = DataForm(request.POST, request.FILES)
        if form.is_valid():
            uploaded_file = form.save(commit=False)  
            uploaded_file.user = request.user  
            uploaded_file.save()  

            raw_text = file_to_txt(uploaded_file.file.path)

            #cleaned_text = clean_text(raw_text)
            #uploaded_file.criterion = extract_outcomes(cleaned_text)
            #uploaded_file.criterion = raw_text

            uploaded_file.criterion = extract_outcomes(raw_text)
            uploaded_file.save()

            request.session['uploadedData_id'] = uploaded_file.data_id  
            return redirect("/questions")  
    else:
        form = DataForm()

    uploaded_files = UploadedData.objects.filter(user=request.user)
    return render(request, "pages/professor/LLmTest.html", {"form": form, "uploaded_files": uploaded_files})


@login_required
def edit_PIs(request):
    # Get the uploaded file object
    uploaded_file = get_object_or_404(UploadedFile)

    if request.method == 'POST':
        form = UploadedFileForm(request.POST, instance=uploaded_file)
        if form.is_valid():
            form.save()  # Save the updated PIs to the database
            return redirect("/setup/")  
    else:
        form = UploadedFileForm(instance=uploaded_file)

    return render(request, "pages/professor/edit_PIs.html", {"form": form, "uploaded_file": uploaded_file})


@login_required
def delete_file(request, data_id):
    if request.method == "POST":
        try:
            file = UploadedData.objects.get(data_id=data_id)  # Use data_id instead of id
            file.delete()
            return JsonResponse({"success": True})
        except UploadedData.DoesNotExist:
            return JsonResponse({"success": False, "error": "File not found"}, status=404)
    return JsonResponse({"success": False, "error": "Invalid request"}, status=400)




@login_required
def AutoAssess(request):
    if request.method == "POST":
        form = UploadFileForm(request.POST, request.FILES)
        if form.is_valid():
            uploaded_file = form.save(commit=False)  # Save the instance without committing yet
            uploaded_file.user = request.user  # Associate with the logged-in user
            uploaded_file.save()  # Save the file to the database and filesystem

            pdf_file = "output.pdf"
            pdf_file = doc_to_pdf(uploaded_file.file.path, pdf_file)

            # Extract raw text from the uploaded PDF
            raw_text = file_to_txt(pdf_file)

            # Clean the extracted text
            cleaned_text = clean_text(raw_text)

            # Extract performance indicators based on the pattern
            performance_indicators = extract_PI_section(cleaned_text)

            # Format extracted content for storage or display
            extracted_content = "\n".join(
                f"{idx + 1}. {pi}"
                for idx, pi in enumerate(performance_indicators)
            )

            # Assign the extracted content to the model
            uploaded_file.extracted_content = extracted_content

            # Assign PIs to specific fields in the UploadedFile model
            for i, pi in enumerate(performance_indicators):
                pi_field = f"PI{i+1}"  
                if hasattr(uploaded_file, pi_field):
                    setattr(uploaded_file, pi_field, pi)  

            uploaded_file.save()  # Save all changes to the database

            return redirect("/edit/")  # Redirect to edit page (modify as needed)
    else:
        form = UploadFileForm()

    uploaded_files = UploadedFile.objects.filter(user=request.user)
    return render(request, "pages/professor/AutoAssess.html", {"form": form, "uploaded_files": uploaded_files})


@login_required
def setupAssess(request):
    uploaded_PIs = get_object_or_404(UploadedFile)

    if request.method == 'POST':
       form = AssessForm()
       if form.is_valid():
            selected_criteria = form.cleaned_data['criterion']
    else:
        form = AssessForm()

    return render(request, "pages/professor/setup.html", {"form" :form})


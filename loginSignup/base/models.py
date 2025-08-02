from django.db import models
from django.conf import settings
from django.contrib.auth.models import AbstractUser

# Create your models here.

class CustomUser(AbstractUser):
    access_field = models.IntegerField()
    program_field = models.IntegerField()

class Course(models.Model):
    #course_id - Ex: CS-1337        Semester, Year, Course  Requirements.txt 
    course_id = models.CharField(primary_key=True, max_length = 100, blank = True, null = False)

    professor = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.CASCADE, null=True, blank=True)

    #year - Ex: 2024, 2025
    year = models.IntegerField(blank = True, null = True)
    #semester - Ex: Spring or Fall
    semester = models.CharField(max_length = 50, blank = True, null = True)
    #program
        # (1, 'Computer Science'),
        # (2, 'Engineering'),
        # (3, 'Business'),
    program = models.IntegerField()


class UploadedData(models.Model):
    data_id = models.AutoField(primary_key=True)  

    user = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.CASCADE, null=True, blank=True)

    file = models.FileField(upload_to='uploads/', null=True, blank=True)
    uploaded_at = models.DateTimeField(auto_now_add=True)  # Track upload time

    course = models.CharField(max_length=50, blank=True, null=True) 
    program = models.CharField(max_length=50, blank=True, null=True)
    
    # Extracted content from file
    extracted_content = models.TextField(blank=True, null=True)

    # Criterion & Questions
    criterion = models.TextField(blank=True, null=True)
    outcomes = models.TextField(blank = True, null = True )
    questions = models.TextField(blank=True, null=True)

    grades = models.TextField(null=True, blank=True)

    results = models.TextField(blank=True, null = True)

    def __str__(self):
        return self.file.name if self.file else "No File"


class Data(models.Model):
    data_id = models.IntegerField(primary_key = True)

    file = models.FileField(upload_to = 'uploads/', null = True, blank = True)

    course_id = models.ForeignKey(Course, on_delete = models.CASCADE, null = True, blank = True)

    program = models.CharField(max_length = 50, blank = True, null = True)

    results = models.TextField(blank=True, null = True)
    

class UploadedFile(models.Model):
    user = models.ForeignKey(settings.AUTH_USER_MODEL, on_delete=models.CASCADE, null=True, blank=True)
    file = models.FileField(upload_to='uploads/')
    uploaded_at = models.DateTimeField(auto_now_add=True)
    extracted_content = models.TextField(blank=True, null=True)  # Ensure this is defined

    Course = models.CharField(max_length=200, blank=True, null=True) 
    PI1 = models.CharField(max_length=500, blank=True, null=True)  
    PI2 = models.CharField(max_length=500, blank=True, null=True)
    PI3 = models.CharField(max_length=500, blank=True, null=True)
    PI4 = models.CharField(max_length=500, blank=True, null=True)
    PI5 = models.CharField(max_length=500, blank=True, null=True)
    PI6 = models.CharField(max_length=500, blank=True, null=True)
    PI7 = models.CharField(max_length=500, blank=True, null=True)
    PI8 = models.CharField(max_length=500, blank=True, null=True)   
    def __str__(self):
        return self.file.name

    @property
    def PIs(self):
        return [self.PI1, self.PI2, self.PI3, self.PI4, self.PI5, self.PI6, self.PI7, self.PI8]


class Assessment(models.Model):
    uploaded_file = models.ForeignKey(UploadedFile, on_delete=models.CASCADE, related_name="assessments")
    course = models.TextField(blank=True, null=True)  
    pi_data = models.JSONField(blank=True, null=True)  

    uploaded_at = models.DateTimeField(auto_now_add=True)
    results = models.IntegerField(null=True, blank=True)
    scores = models.IntegerField(null=True, blank=True)
    target_score = models.IntegerField(null=True, blank=True)
    criterion = models.CharField(max_length = 200, blank = True, null =True)


    def save(self, *args, **kwargs):
        # Automatically populate PIs from the UploadedFile if not set
        if not self.pi_data and self.uploaded_file:
            self.pi_data = {
                f"PI{i+1}": getattr(self.uploaded_file, f"PI{i+1}") for i in range(8)
            }
        super().save(*args, **kwargs)

    def __str__(self):
        return f"Assessment for {self.uploaded_file.file.name}"
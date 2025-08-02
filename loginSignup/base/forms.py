from django import forms
from django.contrib.auth.forms import UserCreationForm
from .models import CustomUser, UploadedFile, Assessment, UploadedData, Course


class CreateUserForm(UserCreationForm):
    access_choices = [
        (1, 'Professor'),
        (2, 'Program Coordinator'),
        (3, 'Admin'),
        ]

    program_choices = [
        (1, 'Computer Science'),
        (2, 'Engineering'),
        (3, 'Business'),
        ]

    email = forms.EmailField(required=True)
    access_field = forms.ChoiceField(choices= access_choices, label ="Select your Role")
    program_field = forms.ChoiceField(choices= program_choices, label ="Select Your Program")
    #access code for Admin Signup
    access_code = forms.CharField(required=False, max_length = 10, widget= forms.PasswordInput, label= "Enter Access Code")
    first_name = forms.CharField(required=False, max_length = 10, label= "Enter your first name")
    last_name = forms.CharField(required=False, max_length= 10, label="Enter your last name")
    

    class Meta:
        model = CustomUser
        fields = ['username', 'email', 'access_field','program_field' , 'password1', 'password2', 'first_name', 'last_name']

    def clean(self):
        cleaned_data = super().clean()
        access_field = cleaned_data.get('access_field')
        access_code = cleaned_data.get('access_code')

        ACCESS_CODE = "SECRET123"  

        # Validate access code if user selects 'Admin'
        if access_field == '3' and access_code != ACCESS_CODE:
            raise forms.ValidationError("Invalid access code for admin registration.")

        if access_field == '2' and access_code != ACCESS_CODE:
            raise forms.ValidationError("Invalid access code for admin registration.")

    
    def save(self, commit=True):
        user = super().save(commit=False)
        user.email = self.cleaned_data['email']
        user.access_field = self.cleaned_data['access_field']
        
        if user.access_field == '3':
            user.is_superuser = True
            user.is_staff = True

        if commit:
            user.save()
        return user
    

class DataForm(forms.ModelForm):
    
    class Meta:
        model = UploadedData
        fields = ['file' , 'criterion', 'outcomes', 'questions' ]



class CourseForm(forms.ModelForm):

    class Meta:
        model = Course
        fields = ['year', 'semester']

        
        
class UpdateUserForm(forms.ModelForm):
    password1 = forms.CharField(
        label="New Password", 
        widget=forms.PasswordInput, 
        required=False
    )
    password2 = forms.CharField(
        label="Confirm New Password", 
        widget=forms.PasswordInput, 
        required=False
    )

    class Meta:
        model = CustomUser
        fields = ['username', 'email']

    def clean(self):
        cleaned_data = super().clean()
        password1 = cleaned_data.get("password1")
        password2 = cleaned_data.get("password2")

        if password1 and password1 != password2:
            raise forms.ValidationError("Passwords do not match.")

        return cleaned_data
    

#unused
class UploadFileForm(forms.ModelForm):
    PI1 = forms.CharField(label="Performance Indicator 1", max_length=255, required=False)
    PI2 = forms.CharField(label="Performance Indicator 2", max_length=255, required=False)
    PI3 = forms.CharField(label="Performance Indicator 3", max_length=255, required=False)
    PI4 = forms.CharField(label="Performance Indicator 4", max_length=255, required=False)
    PI5 = forms.CharField(label="Performance Indicator 5", max_length=255, required=False)
    PI6 = forms.CharField(label="Performance Indicator 6", max_length=255, required=False)
    PI7 = forms.CharField(label="Performance Indicator 7", max_length=255, required=False)
    PI8 = forms.CharField(label="Performance Indicator 8", max_length=255, required=False)

    class Meta:
        model = UploadedFile
        fields = ['file', 'PI1', 'PI2', 'PI3', 'PI4', 'PI5', 'PI6', 'PI7', 'PI8']

class UploadedFileForm(forms.ModelForm):

    class Meta:
        model = UploadedFile
        fields = ['Course', 'PI1', 'PI2', 'PI3', 'PI4', 'PI5', 'PI6', 'PI7', 'PI8']  # Include all the PIs you want to edit

class AssessForm(forms.ModelForm):
    criterion_choices = [
    (1, '1'),
    (2, '2'),
    (3, '3'),
    (4, '4'),
    (5, '5'),
    (6, '6'),
    ]

    criterion = forms.MultipleChoiceField(choices= criterion_choices, widget=forms.CheckboxSelectMultiple,  label ="Choose the Criterion the Performance Indicator Tests", required=True)

    class Meta:
        model = Assessment
        fields = ['criterion' ]
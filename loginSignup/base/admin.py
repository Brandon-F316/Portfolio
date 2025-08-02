from django.contrib import admin

from django.contrib import admin
from django.contrib.auth.admin import UserAdmin as BaseUserAdmin
from .models import Course, Data, CustomUser

class CourseAdmin(admin.ModelAdmin):
    fields = ['course_id', 'professor', 'year', 'semester', 'program']
    list_display = ('course_id','professor', 'year', 'semester', 'program')
    list_filter = ('course_id', 'professor', 'program', 'year','semester')
    
class DataAdmin(admin.ModelAdmin):
    fields = ['data_id', 'file', 'course_id', 'program', 'results']
    list_display = ('program', 'results', 'file')
    list_filter = ('program', 'course_id', 'results', 'file')
    
class CustomUserAdmin(BaseUserAdmin):
    fieldsets = BaseUserAdmin.fieldsets + (
        (None, {'fields': ('access_field', 'program_field')}),
    )
    add_fieldsets = BaseUserAdmin.add_fieldsets + (
        (None, {'fields': ('access_field', 'program_field')}),
    )
    
    list_display = ('username', 'email', 'first_name', 'last_name', 'access_field','program_field', 'is_staff', 'is_superuser')
    list_filter = ('access_field', 'program_field', 'is_staff', 'is_superuser')
    search_fields = ('username','email','access_field','program_field')  
        
admin.site.register(Course, CourseAdmin)
admin.site.register(Data,DataAdmin)
admin.site.register(CustomUser,CustomUserAdmin)
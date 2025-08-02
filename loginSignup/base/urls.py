from django.urls import path , include 

from .views import  authView, home, AutoAssess, logoutUser, UserSettings, delete_file 
from .views import  edit_PIs, setupAssess, ViewData, assessOption, ManAssess, LLm_test, Questions, profile, Courses, ManQuestions, Grades, targetScores

from django.contrib.auth import views as auth_views
from django.conf import settings
from django.conf.urls.static import static

urlpatterns = [
    path("", home, name="home"),
    path("signup/", authView, name="authView"),
    path("settings/", UserSettings,name="settings"),

    path("courses/", Courses,name="courses"),

    path("changepassword/", auth_views.PasswordChangeView.as_view()),
    path("logout/", logoutUser, name="logout"),

    path("accounts/", include("django.contrib.auth.urls")),

    path("assess/", assessOption, name ="assessOption"),

    path("test/", LLm_test, name = "llmTest"),
    path("questions/", Questions, name = "questions"),

    path("ManAssess/", ManAssess, name ="ManAssess"),
    path("Mquestions/", ManQuestions, name = "Mquestions"),
    path("Mgrades/", Grades, name = "grades"),
    path("target/", targetScores, name = "target"),



    path("AutoAssess/", AutoAssess, name ="AutoAssess"),

    path("delete_file/<int:data_id>/", delete_file, name="delete_file"),

    path('edit/', edit_PIs, name="edit"),
    path('setup/', setupAssess, name = "setup" ), 
    path("ViewData/", ViewData, name="ViewData"),
    path("profile/", profile, name="profile" )

]  + static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)
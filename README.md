Docary
======

**This project is no longer alive.** 

I will take the lessons learned, pivot and come back with another _better_ idea. 

What is a Docary?
------------------
Basically it's a very straight-forward personal time-tracking application. It does not really aim towards a certain audience or niche. You can use it for whatever you want, this application doesn't try to focus on the clients you visited, the time you spent studying or the time you spent on certain tasks. 

What can I do with it?
-----------------------
For now, the feature list is very small and focused.

* Use your phone, mobile device or desktop to enter your activities (description, tag, location)
* Get a nice timeline which you can use to review your week and study trends
* Get some general statistics on your time usage

However, this is just an alpha release. I do plan on implementing ways to get more out of your data. I like to believe this data can help you learn about and improve how you spend your time. I also plan on implementing ways to get the raw data, so you can manipulate, study or Excel Pivot it however you like. 

Why did you build this?
-----------------------
I used to use Google Calendar to track my time usage, but that approach didn't really work out for me. Google Calendar focuses on the scheduling part, which isn't what I'm looking for, I want to register my time usage the moment I'm doing it. This does mean it requires some determination to get in the habit of submitting your activities. Also, maybe the biggest problem with Google Calender is that there is no easy way to get your data out. 

Technology stack 
----------------
### General
* ASP.NET MVC3
* Ninject

### Data
* Entity Framework

### Testing
* Moq
* xUnit (Extra assertions)

### UI
* jQuery Mobile
* jQuery UI

### Monitoring/Profiling
* Elmah
* Mvc Mini Profiler

Some screenshots for the sake of documenting
---------------------------------------------
![Desktop/Home/Welcome] (http://jefclaes.github.com/Docary/img/Desktop_Home_Welcome.jpg)
![Desktop/Home/Index] (http://jefclaes.github.com/Docary/img/Desktop_Home_Index.jpg)
![Desktop/Home/Statistics] (http://jefclaes.github.com/Docary/img/Desktop_Home_Welcome.jpg)
![Mobile/Home/Index] (http://jefclaes.github.com/Docary/img/Mobile_Home_Index.jpg)
![Mobile/Home/Add] (http://jefclaes.github.com/Docary/img/Mobile_Home_Add.jpg)
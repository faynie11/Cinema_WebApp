# Cinema_WebApp
A web app with two user roles: regular users can book and manage movie tickets via a step-by-step wizard; admins can add movies, schedule screenings, and manage reservations. Users register with email and can cancel bookings up to 30 mins before showtime.


How to Run the Application?

The application is built using the following technologies:

- ASP.NET 8 Web API
- Angular 18
- PostgreSQL

and the following IDEs: Microsoft Visual Studio and Microsoft Visual Studio Code.

Before running the application, make sure all the technologies are properly installed.
After installing PostgreSQL, launch PgAdmin 4 and set your own password.
Next, in the backend project "CinemaBooking", open the appsettings.json file and set your database password that you used for PostgreSQL:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=cinema;Username=postgres;Password=yourpassword"
}
Then, make sure you have EF Core installed:

dotnet tool install --global dotnet-ef

Apply the migration and create the database:

dotnet ef database update

Steps to run the application:
1. Start PgAdmin 4
2. Using Microsoft Visual Studio, run the backend project CinemaBooking
3. Using Microsoft Visual Studio Code, go to the frontend directory and run the command:
   
ng serve

4. Finally, register the first user. To be able to add movies, you need to assign the user the Admin role directly in the database. In the Users table, change the user role from "User" to "Admin". Then log out and log back in.


Cinema Web Application Description
This web application has two types of users:

Regular User: Has access to the ticket reservation module and their personal profile.

Administrator: Has the ability to add new movies (including title, description, poster, and duration), create screening schedules (assigning movies to specific times and dates), and view users assigned to particular screenings.

User Registration and Profiles
Users register by providing their first name, last name, email address, and password. These details can later be edited from the user's profile, except for the email address, which also serves as the login identifier.

Administrators register in the same way as regular users. However, to become an administrator, their account must be manually assigned the admin role in the data storage layer (e.g., database).

Ticket Reservation
Users can reserve one or multiple seats for a specific screening. Reservations can be canceled up to 30 minutes before the screening starts.

The reservation process is presented in the UI as a multi-step wizard with the following steps:

Movie Selection:
Users choose a movie from a gallery-style view showing movie titles and posters.

Screening Selection:
Users choose a specific screening time for the selected movie on the chosen date.

Seat Selection:
Users view a seating chart of the cinema hall.

Reserved seats are marked and unavailable.
Users can select available seats by clicking on them, and deselect them by clicking again.
Selected seats are visually highlighted.
Users review their reservation details.

Administrators can also cancel any user’s reservation. In such cases, the affected user receives an appropriate email notification.


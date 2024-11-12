# Todo App

Test task for Elixeum! ;)

## This project is using
- .NET 9/ASP.NET
- Vue3 (Options API)
- PostgreSQL
- nginx
- Dapper
- SCSS
- Docker/Docker Compose

# How to run this app
0. Make sure you have Docker and Docker Compose installed
1. Clone this repository
2. Open shell in the cloned repository
3. Run: `docker-compose up --build`
4. Open http://localhost:3000
5. Enjoy!

# How to use this app
You can switch between task list and task creating using the navigation (Home/Create)

## Home
Select what type of tasks you want to see using the provided select input (defaults to All), it will filter all the tasks based on their state

You can now see all of the tasks that passed the filter

You can change task's state using the select input in the task component or you can delete it by clicking the Delete button

## Create
Here you can create tasks using the provided form

You need to specify task's title, it's default state and it's content. When you are ready, click on the Create button to create the task

# Thanks for using the app
- by Marián Marek (flowernal)
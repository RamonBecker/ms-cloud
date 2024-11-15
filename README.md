# ms-cloud


## :information_source: Information 

Microservices project developed with .NET, using MySQL as the chosen database. I created two flowcharts to provide a better understanding of the work accomplished. The first flowchart illustrates how the project was divided and the interactions of each microservice. The second, in a simplified manner, demonstrates how the Identity Server handles user authentication within the system.

Technologies used during development:

- OAuth2 for using temporary access tokens instead of user credentials. These tokens can have specific scopes (defining what can be accessed) and limited lifespans, ensuring enhanced security.

- OpenID Connect to provide an ID Token in JWT (JSON Web Token) format, containing authenticated user information such as name, email, and unique identifier, allowing the application to verify the user's identity.

- Duende Identity Server for centralized authentication (Single Sign-On), enabling users to log in once to access multiple applications, reducing the need for separate logins for each application and offering a Single Sign-On (SSO) experience.

- Duende Identity Server implements OAuth2 and OpenID Connect, enabling authentication and authorization with access tokens and identity tokens, which are fundamental for securing resources and validating user identities.

- Ocelot (API Gateway) to route incoming requests to the microservices, simplifying the complexity of communication between the client and internal services. It also supports authentication and authorization using JWT and integrations with identity providers like IdentityServer, facilitating API security.

- RabbitMQ to create queues and store and organize messages sent by producers so they can be consumed by consumers later, promoting an asynchronous workflow.

## Data flowchart

![GeekShopping Web (2)](https://github.com/user-attachments/assets/8a8e56a4-314e-4603-8f32-72f4ac01e7a8)


## IdentityServer Flowchart

![IdentityServer](https://github.com/user-attachments/assets/454de203-58dd-4e02-a887-12a12df885d9)


## ⚠️ Prerequisite
![ASPNET Badge](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

![Mysql Badge](https://img.shields.io/badge/MySQL-00000F?style=for-the-badge&logo=mysql&logoColor=white)

![openid (1)](https://github.com/user-attachments/assets/c90bb5e2-cc28-4cae-bbac-8688c19bf4ec)

![oauth2 (1)](https://github.com/user-attachments/assets/ccf03096-31af-45ce-b672-c8c108e8ab14)

![duend (1)](https://github.com/user-attachments/assets/96c3b293-42ff-4904-9cf8-ea6a5c86fc1b)

![ocelot (1)](https://github.com/user-attachments/assets/79f843ad-949f-4694-8a2f-85eb0533683e)

![RabbitMQ](https://img.shields.io/badge/Rabbitmq-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white)


## :rocket: Installation

![](https://img.shields.io/badge/Linux-FCC624?style=for-the-badge&logo=linux&logoColor=black)


```
git clone https://github.com/RamonBecker/ms-cloud.git
```

![](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)
```
git clone https://github.com/RamonBecker/ms-cloud.git
or install github https://desktop.github.com/ 
```


To install the project you must install mysql on your machine.

## ⚙️ Installing MySQL

Enter the following commands in the terminal.

```
sudo apt update
sudo apt install mysql-server

```
### Configuring MySQL

For new installations, you will want to run the security script that is included. This changes some of the less secure default options for things like root logins and example users. Enter the command below.

```
sudo mysql_secure_installation
```
This will take you through a series of prompts where you can make some changes to the security options of your MySQL installation. The first prompt will ask you if you want to configure the Validate Password Plugin, which can be used to test the strength of your MySQL password. Regardless of your choice, the next prompt will be to set the password for the MySQL root user. Sign in and then confirm a secure password of your choice.

From there, you can press Y and then ENTER to accept the default answers for all subsequent questions. This will remove some anonymous users and the test database, disable remote login for root, and load all of these new rules so that MySQL immediately respects the changes you made.

### Testing MySQL

To see if MYSQL is running, type the following command.

```
systemctl status mysql.service
```

If MySQL is not running, you can start it with the following command.
```
sudo systemctl start mysql

Now try to connect your root user to MySQL.
```
mysql -u root -p

### Attention when creating the database

At the visual studio terminal enter the following command to create the database. But first check your username in the MYSQL database in the file: appsettings.json

Type the following:
```
Update-Database
```
If there is any conflict with the migrations, delete them and create them again using the command
```
Add-Migration Your message

Update-Database
```



## 🔨 Docker

To install RabbitMQ in Docker container, open the windows cmd or linux terminal and type:
```
docker run -d -p 15672:15672 -p 5672:5672 --name rabbitmq rabbitmq-3-management
```

Once RabbitMQ is installed, you will need to enable the feature to move messages from one queue to another. For this you will need to enter the RabbitMQ bash, type in the terminal:
```
docker exec -it rabbitmq bash
```
Run the following command in bash:
```
rabbitmq-plugins enable rabbitmq_shovel rabbitmq_shovel_management
```


RabbitMQ has as default user:
```
username: guest
password: guest
```
If you want to change the default user, open your browser and enter the following address to enter the RabbitMQ user settings and make the changes you want:
```
http://localhost:15672/#/users/guest
```




## :memo: Developed features

- [x] CRUD Shopping Cart
- [x] CRUD Product
- [x] CRUD Shopping Cart Discount Coupon
- [x] Purchase Order Payment Record
- [x] Creating, consuming and sending queues in RabbitMQ




## :technologist:	 Author

By Ramon Becker 👋🏽 Get in touch!



[<img src='https://cdn.jsdelivr.net/npm/simple-icons@3.0.1/icons/github.svg' alt='github' height='40'>](https://github.com/RamonBecker)  [<img src='https://cdn.jsdelivr.net/npm/simple-icons@3.0.1/icons/linkedin.svg' alt='linkedin' height='40'>](https://www.linkedin.com/in/https://www.linkedin.com/in/ramon-becker-da-silva-96b81b141//)
![Gmail Badge](https://img.shields.io/badge/-ramonbecker68@gmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:ramonbecker68@gmail.com)

# **"Bike Store" - an online platform for choosing and buying bikes**
---
## Agenda
- [Description](#Description)
- [Technologies](#Technologies)
- [Setup](#Setup)
- [Project structural features](#Project-structural-features)
- [Application appearance](#Application-appearance)
    - [Customer area](#Customer-area)
    - [Seller area](#Seller-area)
    - [DB Administration area](#DB-Administration-area)
- [License](#License)
---

## **Description**
"Bike Store" is an ASP.NET Core application that combines two subsystems: the client and the admin. The customer/client has everything they need to conveniently choose and buy a bicycle. The admin/seller has efficient tools and user-friendly design for administering the store database.

---

## **Technologies**
- ASP.Net Core 3.1 (MVC)
- Entity Framework Core 3.1
- Bootstrap 4.3.1
- JQuery 3.5.1

## **Setup**
To run this project, download it and open with Visual Studio. Before start up ypu should specify database connection string in file **appsetting.json**.
```
"ConnectionStrings": {
    "DefaultConnection": "PLACE IT HERE"
},
```
Then build and run project.
>Note: In case if database doesn't exist, app will create and configure it.

By default database has three roles (Admin, Seller, Customer) and two users:
- administrator (Email: admin.bikestore@gmail.com, Password: P@ssword1)
- shop assistant (Email: seller.bikestore@gmail.com, Password: P@ssword1)

---

## **Project structural features**
In this section there are detailed information about functions and appearance of the application pages
- Using ASP.Net Core Areas to separate functionality
- Using "Fat Controllers" (better place BL in services instead)
- Using Filters to separate users access 
- Using Attributes for convenient use of authorization filters and data validation
- Using RazorPages to implement DB administer functionality

---

## **Application appearance**

### **Customer area**
This section is dedicated to customer pages

#### **Start Page**
This page has information on the latest store news, sales and supplies

![alt text](img/home_page.png?raw=true)

#### **Catalog Page**
This page displays product cards, and provides controls for searching, sorting and filtering user requests

![alt text](img/product_page.png?raw=true)

#### **Product Page**
This page displays detailed information on chosen bike model, its colours, available sizes and provides controls for adding this product to user cart

![alt text](img/product_details_page.png?raw=true)

#### **Cart Page**
This page displays shorten information on the chosen bikes and provides controls for managing cart content, ordering

![alt text](img/cart_page.png?raw=true)

#### **Order Page**
At this page customer have to enter delivery address information

![alt text](img/order_page.png?raw=true)

#### **Checkout Page**
This page displays information about the checkout.

![alt text](img/checkout_page.png?raw=true)

#### **Checkout Email**
After a successful order, the customer will receive an order confirmation email. This letter provides an abbreviated customer order information

![alt text](img/checkout_email.png?raw=true)

#### **Login Page**
This page provides a login form for already registered users

![alt text](img/login_page.png?raw=true)

#### **Register Page**
Thi page provides a register form for new users

![alt text](img/register_page.png?raw=true)

#### **Confirm E-mail Page**
This page informs about the need to confirm an email address

![alt text](img/confirm_email_page.png?raw=true)

#### **Confirmation Email**
This email provides a link for account confirmation

![alt text](img/confirmation_email.png?raw=true)

### **Seller area**
This section is dedicated to seller pages
Only shop staff (sellers and administrators) have access to these pages

#### **Administration Page**
This page provides links to control pages

> Note: Only administrators have access to lower section pages

![alt text](img/administrator_page.png?raw=true)

#### **Models Page**
This page provides model cards with shorten information on model name, amount of all and reserved bikes

![alt text](img/models_page.png?raw=true)

#### **Model Details Page**
This page displays detailed information on chosen bike model, its colours, available sizes and provides controls for editing, deleting model info and creating model colours schemes

![alt text](img/model_details_page.png?raw=true)

#### **Model Create Page**
This page provides a form for creating model

![alt text](img/model_create_page.png?raw=true)

#### **Model Edit Page**
This page provides a form for editing model info

![alt text](img/model_edit_page.png?raw=true)

#### **Model-Colour Scheme Create Page**
This page provides a form for creating model-colour scheme

![alt text](img/model_colour_create_page.png?raw=true)

#### **Model-Colour Scheme Edit Page**
This page provides a form for editing model-colour scheme info

![alt text](img/model_colour_edit_page.png?raw=true)

#### **Supply Create Page**
This page provides a form for adding information on supply 

![alt text](img/supply_header_page.png?raw=true)

#### **Supply Details Page**
This page has information about bikes in current supply, and provides controls for editing and deleting this information

![alt text](img/supply_bikes_page.png?raw=true)

#### **Bike Create Page**
This page provides a form for creating bike

![alt text](img/bike_create_page.png?raw=true)

#### **Bike Edit Page**
This page provides a form for editing bike info

![alt text](img/bike_edit_page.png?raw=true)

#### **Supplies History Page**
This page has information on all supplies

![alt text](img/supply_history_page.png?raw=true)

#### **Orders History Page**
This page has information on all orders

![alt text](img/order_history_page.png?raw=true)

#### **Mailing Page**
This page provides a form for creating mailing for users with "Allow mailing" setting

![alt text](img/emailing_page.png?raw=true)

### **DB Administration area**
This section is dedicated to db administration pages
Only administrators have access to pages from this section
All pages in this section have a similar UI and purpose, the main difference - managing different db record types

#### **Age Groups Page**
This page provides CRUD UI for "Age Groups" managing

![alt text](img/age_groups_page.png?raw=true)

#### **Categories Page**
This page provides CRUD UI for "Categories" managing

![alt text](img/categories_page.png?raw=true)

#### **Colours Page**
This page provides CRUD UI for "Colours" managing

![alt text](img/colours_page.png?raw=true)

#### **Frame Sizes Page**
This page provides CRUD UI for "Frame Sizes" managing

![alt text](img/frame_sizes_page.png?raw=true)

#### **Model Names Page**
This page provides CRUD UI for "Model Names" managing

![alt text](img/model_names_page.png?raw=true)

#### **Model Prefixes Page**
This page provides CRUD UI for "Model Prefixes" managing

![alt text](img/model_prefixes_page.png?raw=true)

#### **Sexes Page**
This page provides CRUD UI for "Sexes" managing

![alt text](img/sexes_page.png?raw=true)

#### **Statuses Page**
This page provides CRUD UI for "Statuses" managing

![alt text](img/statuses_page.png?raw=true)

#### **Storing Places Page**
This page provides CRUD UI for "Storing Places" managing

![alt text](img/storing_places_page.png?raw=true)

#### **Suspensions Page**
This page provides CRUD UI for "Suspensions" managing

![alt text](img/suspensions_page.png?raw=true)

---

## **License**

BikeSore is under [MIT](LICENSE)

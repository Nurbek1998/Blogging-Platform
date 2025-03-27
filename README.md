# 📈 Blogging Platform API
<br>


**Modern & Scalable Blog Management System**

<br>
🚀 RESTful API for creating and managing blogs, categories, tags, comments, and user interactions!
<br> <br>

🔹 **Technologies:** `.NET 8`, `ASP.NET Core`, `Entity Framework Core`, `PostgreSQL`, `JWT Authentication`, `SignalR`  
🔹 **Features:** `CRUD Operations`, `Role-Based Authentication`, `Real-Time Notifications`, `FluentValidation`

<br>

---

## ⚡ Features

✔️ Create, edit, and delete blog posts  
✔️ Categorize and tag posts  
✔️ Comment, like, and interact with posts  
✔️ Role-based authentication (`Admin`, `Author`, `Moderator`,'User')  
✔️ Real-time notifications (SignalR)  
✔️ Secure API with `JWT Authentication`

<br>

---

## 📥 Installation & Setup

```bash
git clone https://github.com/Nurbek1998/Blogging-Platform.git
cd Blogging-Platform
dotnet restore
dotnet ef database update
dotnet run
```
<br>

## 🚀 API Endpoints

### 📝 Posts

| Method   | Endpoint          | Description       |
| -------- | ----------------- | ----------------- |
| `POST`   | `/api/posts`      | Create a new post |
| `GET`    | `/api/posts/{id}` | Get post by ID    |
| `PUT`    | `/api/posts/{id}` | Update post       |
| `DELETE` | `/api/posts/{id}` | Delete post       |

<br>

### 📌 Categories & Tags

| Method | Endpoint          | Description           |
| ------ | ----------------- | --------------------- |
| `GET`  | `/api/categories` | Get all categories    |
| `POST` | `/api/categories` | Create a new category |
| `GET`  | `/api/tags`       | Get all tags          |
| `POST` | `/api/tags`       | Create a new tag      |

<br>

### 📝 Comments

| Method   | Endpoint          | Description       |
| -------- | ----------------- 	| ----------------- |
| `POST`   | `/api/comments`      | Create a comment |
| `GET`    | `/api/comments/{id}` | Get comment by ID    |
| `PUT`    | `/api/comments/{id}` | Update comment       |
| `DELETE` | `/api/comments/{id}` | Delete comment       |

<br>

### 👥 Users & Authentication

| Method | Endpoint             | Description                |
| ------ | -------------------- | -------------------------- |
| `POST` | `/api/auth/register` | Register a new user        |
| `POST` | `/api/auth/login`    | Authenticate and get token |
| `GET`  | `/api/users/profile` | Get user profile           |

<br>

### 🔒 Authentication
- **Endpoint:** `POST /api/auth/register`
- **Request Body:**
```json
  {
    "username": "testuser",
    "password": "Secure123!",
    "role": "Author"
  }
  ```


### 📌 Include the JWT token in the request headers:
```json
  {
    "Authorization: Bearer your_token_here"
  }
  ```

<br>

### 📜 License

This project is licensed under the MIT License.

#### 📧 Contact: [rajabov0553@gmail.com](mailto:rajabov0553@gmail.com)
#### 🔗 GitHub Repo: [Click Here](https://github.com/Nurbek1998/Blogging-Platform)

# Blogging Platform

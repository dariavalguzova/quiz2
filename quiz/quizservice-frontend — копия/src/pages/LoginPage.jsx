import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Login() {

const navigate=useNavigate();

const[email,setEmail]=useState("");
const[password,setPassword]=useState("");

async function handleLogin(e){

e.preventDefault();

try{

const response=await fetch(
"https://localhost:7267/api/auth/login",
{
method:"POST",

headers:{
"Content-Type":"application/json"
},

body:JSON.stringify({
email,
password
})

});

const data=await response.json();

if(response.ok){

localStorage.setItem(
"token",
data.token
);

localStorage.setItem(
"name",
data.name
);

localStorage.setItem(
"role",
data.role
);

alert("Вход выполнен");

if(data.role==="Teacher")
navigate("/teacher");

else if(data.role==="Admin")
navigate("/admin");

else
navigate("/quiz");

}
else{

alert(data);

}

}
catch{

alert("Ошибка сервера");

}

}

return(

<div className="container mt-5">

<div className="card p-4">

<h2>Вход</h2>

<form onSubmit={handleLogin}>

<input
className="form-control mb-3"
placeholder="Email"
value={email}
onChange={(e)=>
setEmail(e.target.value)}
/>

<input
type="password"
className="form-control mb-3"
placeholder="Пароль"
value={password}
onChange={(e)=>
setPassword(e.target.value)}
/>

<button
className="btn btn-primary">

Войти

</button>

</form>

</div>

</div>

)

}

export default Login;
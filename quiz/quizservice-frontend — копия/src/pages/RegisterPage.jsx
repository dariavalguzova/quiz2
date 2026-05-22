/*import { useState } from "react";
import api from "../services/api";

function Register() {

const [name,setName]=useState("");
const [email,setEmail]=useState("");
const [password,setPassword]=useState("");

const register=async()=>{

try{

await api.post("/auth/register",{

name,
email,
password

});

alert("Регистрация успешна");

}
catch(error){

alert("Ошибка");

console.log(error);

}

};

return(

<div className="container mt-5">

<div className="card p-4">

<h1>Регистрация</h1>

<input
className="form-control mb-3"
placeholder="Имя"
onChange={(e)=>setName(e.target.value)}
/>

<input
className="form-control mb-3"
placeholder="Email"
onChange={(e)=>setEmail(e.target.value)}
/>

<input
type="password"
className="form-control mb-3"
placeholder="Пароль"
onChange={(e)=>setPassword(e.target.value)}
/>

<button
className="btn btn-primary"
onClick={register}
>

Зарегистрироваться

</button>

</div>

</div>

);

}

export default Register;*/

function Register(){

return(

<div>

<h1>
Регистрация
</h1>

</div>

)

}

export default Register;
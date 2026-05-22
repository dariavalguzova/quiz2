import {
BrowserRouter,
Routes,
Route
}
from "react-router-dom";

import HomePage from "./pages/HomePage";
import Login from "./pages/LoginPage";
import Register from "./pages/RegisterPage";
import QuizPage from "./pages/QuizPage";

function App(){

return(

<BrowserRouter>

<Routes>

<Route
path="/"
element={<HomePage/>}
/>

<Route
path="/login"
element={<Login/>}
/>

<Route
path="/register"
element={<Register/>}
/>

<Route
path="/quiz"
element={<QuizPage/>}
/>

</Routes>

</BrowserRouter>

)

}

export default App;
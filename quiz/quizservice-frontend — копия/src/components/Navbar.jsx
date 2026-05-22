import { Link } from "react-router-dom";

function Navbar() {
    return (

        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">

            <div className="container">

                <Link className="navbar-brand" to="/">
                    QuizServiceApp
                </Link>

                <div className="navbar-nav">

                    <Link className="nav-link" to="/">
                        Главная
                    </Link>

                    <Link className="nav-link" to="/login">
                        Вход
                    </Link>

                    <Link className="nav-link" to="/register">
                        Регистрация
                    </Link>

                </div>

            </div>

        </nav>
    );
}

export default Navbar;
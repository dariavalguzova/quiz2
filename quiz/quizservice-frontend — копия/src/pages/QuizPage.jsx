import { useEffect, useState } from "react";
import axios from "axios";

function QuizPage() {

    const [questions,setQuestions]=useState([]);

    const [answers,setAnswers]=useState({});

    const quizId=1;


    useEffect(()=>{

        loadQuiz();

    },[]);


    async function loadQuiz(){

        try{

            const response=
            await axios.get(
            `https://localhost:7267/api/quiz/${quizId}`
            );

            setQuestions(response.data);

        }
        catch(error){

            console.log(error);

        }
    }


    function selectAnswer(questionId,answerId){

        setAnswers(prev=>({

            ...prev,

            [questionId]:answerId

        }));

    }


    async function finishQuiz(){

        const data={

            quizId:quizId,

            answers:
            Object.entries(answers)
            .map(([questionId,answerId])=>
            ({
                questionId:Number(questionId),

                answerId:answerId
            }))
        };

        try{

            const token=
            localStorage.getItem("token");

            const result=
            await axios.post(

                "https://localhost:7267/api/student/submit",

                data,

                {
                    headers:
                    {
                        Authorization:
                        `Bearer ${token}`
                    }
                }

            );

            alert(
            `Ваш результат: ${result.data.score}`
            );

        }

        catch(error){

            console.log(error);

        }

    }


    return(

        <div style={{padding:"30px"}}>

            <h1>
                Прохождение теста
            </h1>

            {
                questions.map(question=>(

                    <div
                    key={question.id}

                    style={{
                        border:"1px solid gray",
                        padding:"20px",
                        marginBottom:"20px"
                    }}
                    >

                    <h3>
                    {question.text}
                    </h3>


                    {
                        question.answers
                        .map(answer=>(

                        <div
                        key={answer.id}
                        >

                        <input

                        type="radio"

                        name={`q${question.id}`}

                        onChange={()=>

                        selectAnswer(
                        question.id,
                        answer.id
                        )}

                        />

                        {answer.text}

                        </div>

                        ))
                    }

                    </div>

                ))
            }


            <button
            onClick={finishQuiz}
            >
            Завершить тест
            </button>

        </div>

    )

}

export default QuizPage;
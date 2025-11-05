import { useNavigate } from 'react-router-dom'

function App() {
    const navigate = useNavigate()

    return (
        <div className="flex items-center justify-center h-screen w-screen bg-amber-400">
            <div className="flex flex-col items-center">
                <h1 className="text-4xl font-bold mb-8 drop-shadow-xl">Welkom bij Grocy</h1>
                <div className="space-x-4">
                    <button
                        onClick={() => navigate('/login')}
                        className="px-6 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
                    >
                        Inloggen
                    </button>
                    <button
                        onClick={() => navigate('/register')}
                        className="px-6 py-2 bg-gray-300 text-white rounded hover:bg-gray-400"
                    >
                        Registreren
                    </button>
                </div>
            </div>
        </div>
    )
}

export default App

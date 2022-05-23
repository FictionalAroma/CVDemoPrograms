import './App.css';
import Navbar from './layoutComponents/Navbar';

import {Route, Switch} from 'react-router-dom';
import GamePage from '../pages/GamePage';
import ExampleTest from '../pages/ExampleTest';
import HomePage from '../pages/HomePage';


function App() {

  return (
    <div className="App">
      <header>
      <Navbar />
      </header>
      <main>
        <Switch>
      <Route path="/game">
        <GamePage />  
      </Route>
      <Route path="/Test">
        <ExampleTest />  
      </Route>
      <Route path="/" exact>
        <HomePage />  
      </Route>
      </Switch>
      </main>
    </div>
  );
}

export default App;

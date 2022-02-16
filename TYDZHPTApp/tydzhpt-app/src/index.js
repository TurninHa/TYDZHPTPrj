import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import reportWebVitals from './reportWebVitals';
import "antd/dist/antd.css"
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import asyncLoadComponet from "./Common/loadComponet";

ReactDOM.render(
  <React.StrictMode>
    <Router>
      <Switch>
        <Route path="/" exact render={props => {
          const Login = asyncLoadComponet(() => import("./Login"));
          return <Login {...props}></Login>;
        }}></Route>
        <Route path="/layout" render={props => {
          const Layout = asyncLoadComponet(() => import("./Layout/layoutpage"));
          return <Layout {...props}>
            <Route path="/layout/cdgl" exact render={props => {
              const Cdgl = asyncLoadComponet(() => import("./menu/cdgl"));
              return <Cdgl {...props}></Cdgl>
            }}></Route>
          </Layout>;
        }}></Route>
      </Switch>
    </Router>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

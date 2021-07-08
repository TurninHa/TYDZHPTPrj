import React from "react";

const asyncLoadComponet = loadComponetFunc => (
    class ComponetCls extends React.Component {
        constructor(props) {
            super(props)
            this.state = {
                Component: null
            }
        }

        componentDidMount()
        {
            this.loadComponet();   
        }

        loadComponet() {

            loadComponetFunc()
                .then(module => module.default)
                .then(Component => {
                    this.setState({ Component });
                })
                .catch((err) => {
                    console.error(`Cannot load component in <AsyncComponent />`);
                    throw err;
                });
        }

        render() {
            const { Component } = this.state;
            return Component ? <Component {...this.props} /> : null;
        }
    }
);

export default asyncLoadComponet;
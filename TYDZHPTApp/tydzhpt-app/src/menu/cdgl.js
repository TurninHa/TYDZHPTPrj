import React from "react";
import "../Css/glb.css";
import SearchTool from "./component/PageSearch";
import ListComponent from "./component/PageList";

class MenuMng extends React.Component {
    constructor(props) {
        super(props)
        this.state = { searchCondition: {} }
    }

    setSearchCondition(condition = {}) {
        this.setState({ searchCondition: condition });
    }

    render() {
        return (
            <div className="list-page-container">
                <div className="search-container">
                    <SearchTool setCondition={(condition) => this.setSearchCondition(condition)}></SearchTool>
                </div>
                <div className="list-container">
                    <ListComponent condition={this.state.searchCondition}></ListComponent>
                </div>
            </div>
        );
    }
}

export default MenuMng;
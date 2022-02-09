import React, { Suspense } from "react";
import "../Css/glb.css";
import { Spin } from "antd";
// import SearchTool from "./component/PageSearch";
// import ListComponent from "./component/PageList";

const SearchTool = React.lazy(() => import("./component/PageSearch"));
const ListComponent = React.lazy(() => import("./component/PageList"));

class MenuMng extends React.Component {
    constructor(props) {
        super(props)
        this.state = { searchCondition: {}, nodeValue: -1 }
    }

    setSearchCondition(condition = {}) {
        console.log("查询条件")
        console.log(condition);
        this.setState({ searchCondition: condition });
    }

    resetHandle() {
        console.log("点击了重置");
        this.setState({ nodeValue: Math.random() });
    }

    render() {

        return (
            <div className="list-page-container">
                <Suspense fallback={<Spin></Spin>}>
                    <div className="search-container">
                        <SearchTool setCondition={(condition) => this.setSearchCondition(condition)} reset={() => { this.resetHandle() }}></SearchTool>
                    </div>
                    <div className="list-container">
                        <ListComponent condition={this.state.searchCondition} nodeValue={this.state.nodeValue}></ListComponent>
                    </div>
                </Suspense>
            </div>
        );
    }
}

export default MenuMng;
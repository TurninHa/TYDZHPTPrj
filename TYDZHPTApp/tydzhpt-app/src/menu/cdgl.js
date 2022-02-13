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
        this.state = {
            searchCondition: {
                cdmc: "",
                cdbm: "",
            }
        }
    }

    setSearchCondition(condition = {}) {
        console.log("查询条件")
        console.log(condition);
        this.setState({ searchCondition: condition });
    }

    render() {

        return (
            <div className="list-page-container">
                <Suspense fallback={<Spin></Spin>}>
                    <div className="search-container">
                        <SearchTool setCondition={(condition) => this.setSearchCondition(condition)}></SearchTool>
                    </div>
                    <div className="list-container">
                        <ListComponent condition={this.state.searchCondition}></ListComponent>
                    </div>
                </Suspense>
            </div>
        );
    }
}

export default MenuMng;
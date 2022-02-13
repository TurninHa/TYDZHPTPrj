import React from "react";
import { Input, Button } from "antd"

function PageSearchContainer(props) {

    const setSearchCondition = props.setCondition;

    let cdmc = React.createRef();
    let cdbm = React.createRef();

    //重置
    const resetHandle = () => {
        cdbm.current.input.value = "";
        cdmc.current.input.value = "";

        let condition = {
            cdmc: "",
            cdbm: "",
            reset: true
        };

        setSearchCondition(condition);
    };
    //查询
    const searchHandle = () => {
        let condition = {
            cdmc: cdmc.current.input.value,
            cdbm: cdbm.current.input.value,
            reset: false
        };

        setSearchCondition(condition);
    };

    return (
        <div className="search-tool-container">
            <div className="search-tool-item">
                <div className="search-tool-item-content">
                    <div className="search-tool-item-content-left">
                        <label>菜单名称:</label>
                    </div>
                    <div className="search-tool-item-content-right">
                        <Input ref={cdmc}></Input>
                    </div>
                </div>
            </div>
            <div className="search-tool-item">
                <div className="search-tool-item-content">
                    <div className="search-tool-item-content-left">
                        <label>菜单编码:</label>
                    </div>
                    <div className="search-tool-item-content-right">
                        <Input ref={cdbm}></Input>
                    </div>
                </div>
            </div>
            <div className="search-tool-item-action">
                <div className="search-tool-item-action-content">
                    <div>
                        <Button onClick={() => resetHandle()}>重置</Button>
                    </div>
                    <div>
                        <Button type="primary" onClick={() => searchHandle()}>查询</Button>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default PageSearchContainer;
import React, { useState } from "react";
import { Input, Button } from "antd"

function PageSearchContainer(props) {

    const setSearchCondition = props.setCondition;

    const [cdmc, setCdmc] = useState("");
    const [cdbm, setCdbm] = useState("");

    //重置
    const resetHandle = () => {

        setCdmc("");
        setCdbm("");
        let condition = {
            cdmc: "",
            cdbm: "",
            search: true,
            reset: true
        };

        setSearchCondition(condition);
    };
    //查询
    const searchHandle = () => {
        let condition = {
            cdbm,
            cdmc,
            search: true,
            reset: false
        };

        setSearchCondition(condition);
    };

    const cdmcChangeHandle = e => {
        setCdmc(e.target.value);
    }

    const cdbmChangeHandle = e => {
        setCdbm(e.target.value);
    }

    return (
        <div className="search-tool-container">
            <div className="search-tool-item">
                <div className="search-tool-item-content">
                    <div className="search-tool-item-content-left">
                        <label>菜单名称:</label>
                    </div>
                    <div className="search-tool-item-content-right">
                        <Input value={cdmc} onChange={cdmcChangeHandle}></Input>
                    </div>
                </div>
            </div>
            <div className="search-tool-item">
                <div className="search-tool-item-content">
                    <div className="search-tool-item-content-left">
                        <label>菜单编码:</label>
                    </div>
                    <div className="search-tool-item-content-right">
                        <Input value={cdbm} onChange={cdbmChangeHandle}></Input>
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
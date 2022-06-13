import { useEffect, useState } from "react";
import { Tree, Button, Space } from "antd";
import { menuTree } from "../Api/cdglApi"

export const SetRoleRight = (props) => {

    //通过属性设置的角色编码
    let roleCode = props.Code;
    let roleName = props.Name;

    const [rightData, setRightData] = useState([]);
    const [sltKeys, setSltKeys] = useState([]);

    const loadMenuList = () => {
        menuTree().then(resp => {
            setRightData(resp.data.Data);
        }).catch(er => {
            console.error(er);
        });
    };

    const loadSelectedNodes = () => {

    }

    useEffect(() => {
        loadMenuList();
        loadSelectedNodes();
    }, []);

    return (
        <>
            <div>
                正在为{roleName}设置权限
            </div>
            <Tree
                treeData={rightData}
                selectedKeys={sltKeys}
            >
            </Tree>
            <div>
                <Button>保存</Button>
                <Button>取消</Button>
            </div>
        </>
    );
}
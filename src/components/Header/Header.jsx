import { Avatar, Flex } from "antd";
import React from "react";
import { NotificationIcon } from "~/components/CustomeIcon/CustomeIcon";
import { DownOutlined } from "@ant-design/icons";
import "./Header.css";

const Header = (props) => {
  const { title, subTitle } = props;

  return (
    <>
      <div className="header">
        <Flex align="center" justify="space-between">
          <div className="header__left">
            <p className="header__title">{title}</p>
            <p className="header__title--sub">{subTitle}</p>
          </div>

          <div className="header__right">
            <div className="header__notification">
              <NotificationIcon style={{ fontSize: "20px" }} />
            </div>

            <div className="header__account">
              <Avatar
                src="https://upload.wikimedia.org/wikipedia/vi/e/ec/%C3%81p_ph%C3%ADch_phim_Kuruki_Yuuyami_no_Scherzo.jpg"
                className="header__account--avatar"
                shape="square"
                size={40}
              />
              <div className="header__account--info">
                <p className="header__account--name">Long</p>
                <p className="header__account--role">HR Manager</p>
              </div>
              <DownOutlined />
            </div>
          </div>
        </Flex>
      </div>
    </>
  );
};

export default Header;

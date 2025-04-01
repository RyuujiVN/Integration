import { Flex } from "antd";
import React from "react";

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

          <div className="header__right"></div>
        </Flex>
      </div>
    </>
  );
};

export default Header;

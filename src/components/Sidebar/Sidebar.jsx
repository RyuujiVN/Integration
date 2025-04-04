import Sider from "antd/es/layout/Sider";
import React, { useContext } from "react";
import Logo from "~/assets/images/Logo.png";
import {
  ApplicantIcon,
  AttendanceIcon,
  DashboardIcon,
  DepartmentIcon,
  EmployeeIcon,
  JobIcon,
  PayrollIcon,
} from "~/components/CustomeIcon/CustomeIcon";
import { Menu, Segmented } from "antd";
import "./Siderbar.css";
import { Link } from "react-router-dom";
import { SunOutlined, MoonOutlined } from "@ant-design/icons";
import { ThemeContext } from "~/context/themeContext";

const items = [
  {
    key: "dashboard",
    icon: <DashboardIcon />,
    label: <Link to="/dashboard">Dasboard</Link>,
  },

  {
    key: "employee",
    icon: <EmployeeIcon />,
    label: <Link to="/employee">Nhân viên</Link>,
  },

  {
    key: "department",
    icon: <DepartmentIcon />,
    label: <Link to="/department">Phòng ban</Link>,
  },

  {
    key: "attendance",
    icon: <AttendanceIcon />,
    label: <Link to="/attendance">Chấm công</Link>,
  },

  {
    key: "payroll",
    icon: <PayrollIcon />,
    label: <Link to="/payroll">Bảng lương</Link>,
  },

  {
    key: "job",
    icon: <JobIcon />,
    label: <Link to="/job">Jobs</Link>,
  },

  {
    key: "applicant",
    icon: <ApplicantIcon />,
    label: <Link to="/applicant">Ứng tuyển</Link>,
  },
];

const Sidebar = () => {
  const { myTheme, toggleTheme } = useContext(ThemeContext);

  const handleChange = (value) => {
    toggleTheme(value);
  };

  return (
    <>
      <Sider theme="light" className="sidebar">
        <div className="sidebar__logo">
          <img src={Logo} alt="Logo" width={123} />
        </div>
        <Menu
          defaultSelectedKeys={[location.pathname.split("/")[1] || "dashboard"]}
          mode="inline"
          theme="light"
          items={items}
        />
        <Segmented
          size="large"
          options={[
            { label: "Light", value: "light", icon: <SunOutlined /> },
            { label: "Dark", value: "dark", icon: <MoonOutlined /> },
          ]}
          onChange={handleChange}
          style={{ margin: "auto 10px 10px 10px" }}
          defaultValue={myTheme}
        />
      </Sider>
    </>
  );
};

export default Sidebar;

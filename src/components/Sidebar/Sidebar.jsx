import Sider from "antd/es/layout/Sider";
import React from "react";
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
import { Menu } from "antd";
import "./Siderbar.css";
import { Link } from "react-router-dom";

const items = [
  {
    key: "dashboard",
    icon: <DashboardIcon />,
    label: <Link to="/dashboard">Dasboard</Link>,
  },

  {
    key: "employee",
    icon: <EmployeeIcon />,
    label: <Link to="/employee">Employee</Link>,
  },

  {
    key: "department",
    icon: <DepartmentIcon />,
    label: <Link to="/department">Department</Link>,
  },

  {
    key: "attendance",
    icon: <AttendanceIcon />,
    label: <Link to="/attendance">Attendance</Link>,
  },

  {
    key: "payroll",
    icon: <PayrollIcon />,
    label: <Link to="/payroll">Payroll</Link>,
  },

  {
    key: "job",
    icon: <JobIcon />,
    label: <Link to="/job">Jobs</Link>,
  },

  {
    key: "applicant",
    icon: <ApplicantIcon />,
    label: <Link to="/applicant">Applicants</Link>,
  },
];

const Sidebar = () => {
  return (
    <>
      <Sider theme="light" className="sidebar">
        <div className="sidebar__logo">
          <img src={Logo} alt="Logo" width={123} />
        </div>
        <Menu
          defaultSelectedKeys={["dashboard"]}
          mode="inline"
          theme="light"
          items={items}
        />
      </Sider>
    </>
  );
};

export default Sidebar;

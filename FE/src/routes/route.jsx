import DefaultLayout from "~/components/Layouts/DefaultLayout";
import Dashboard from "~/pages/Dashboard/Dashboard";
import Department from "~/pages/Department/Department";
import AddEmployee from "~/pages/Employee/AddEmployee";
import EditEmployee from "~/pages/Employee/EditEmployee";
import Employee from "~/pages/Employee/Employee";
import ListEmployee from "~/pages/Employee/ListEmployee";
import Position from "~/pages/Position/Position";
import Login from "~/pages/Login/Login";
import Payroll from "~/pages/Payroll/Payroll";
import Applicant from "~/pages/Applicant/Applicant";

const route = [
  {
    path: "/",
    element: <DefaultLayout />,
    children: [
      {
        path: "/dashboard",
        element: <Dashboard />,
      },

      {
        path: "/employee",
        element: <Employee />,
        children: [
          {
            path: "",
            element: <ListEmployee />,
          },

          {
            path: "add",
            element: <AddEmployee />,
          },

          {
            path: "edit",
            element: <EditEmployee />,
          },
        ],
      },

      {
        path: "/department",
        element: <Department />,
      },

      {
        path: "/payroll",
        element: <Payroll />,
      },

      {
        path: "/position",
        element: <Position />,
      },

      {
        path: "/applicant",
        element: <Applicant />,
      },
    ],
  },

  {
    path: "/login",
    element: <Login />,
  },
];

export default route;

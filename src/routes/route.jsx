import DefaultLayout from "~/components/Layouts/DefaultLayout";
import Dashboard from "~/pages/Dashboard/Dashboard";
import Department from "~/pages/Department/Department";
import AddEmployee from "~/pages/Employee/AddEmployee";
import Employee from "~/pages/Employee/Employee";
import ListEmployee from "~/pages/Employee/ListEmployee";
import Login from "~/pages/Login/Login";
import Payroll from "~/pages/Payroll/Payroll";

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
    ],
  },

  {
    path: "/login",
    element: <Login />,
  },
];

export default route;

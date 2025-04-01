import DefaultLayout from "~/components/Layouts/DefaultLayout";
import Dashboard from "~/pages/Dashboard/Dashboard";
import Login from "~/pages/Login/Login";

const route = [
  {
    path: "/",
    element: <DefaultLayout />,
    children: [
      {
        path: "/",
        element: <Dashboard />,
      },
    ],
  },

  {
    path: "/login",
    element: <Login />,
  },
];

export default route;

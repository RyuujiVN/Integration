import Header from "~/components/Header/Header";
import React, { useEffect, useState } from "react";
import { Button, Card, Flex, Form, Input, Popconfirm, Table } from "antd";
import {
  EyeOutlined,
  EditOutlined,
  DeleteOutlined,
  SearchOutlined,
  PlusCircleOutlined,
} from "@ant-design/icons";
import DetailDepartment from "~/pages/Department/DetailDepartment";
import "./Department.css";
import AddDepartment from "./AddDepartment";
import { EditDepartment } from "./EditDepartment";
import {
  deleteDepartment,
  fetchDepartmentDeleteApi,
  fetchDepartmentDetailsApi,
} from "~/redux/department/departmentSlice";
import { useDispatch, useSelector } from "react-redux";
import { toast } from "react-toastify";

const columns = [
  { title: "Tên phòng ban", dataIndex: "name" },
  { title: "Hành động", dataIndex: "action" },
];

const Department = () => {
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);
  const [openDetail, setOpenDetail] = useState(false);
  const [openAddDepartment, setOpenAddDepartment] = useState(false);
  const [openEditDepartment, setOpenEditDepartment] = useState(false);
  const [department, setDepartment] = useState(null);
  const dispatch = useDispatch();

  const departments = useSelector(
    (state) => state.department.currentDepartment
  );

  const handleOpenModal = (setOpen, department) => {
    setOpen(true);
    setDepartment(department);
  };

  const handleDelete = (departmentID) => {
    toast.promise(dispatch(fetchDepartmentDeleteApi(departmentID)), {
      pending: "Đang xoá...",
      success: "Xoá phòng ban thành công!",
    });
    dispatch(deleteDepartment(departmentID));
  };

  const onSelectChange = (newSelectedRowKeys) => {
    console.log("selectedRowKeys changed: ", newSelectedRowKeys);
    setSelectedRowKeys(newSelectedRowKeys);
  };
  const rowSelection = {
    selectedRowKeys,
    onChange: onSelectChange,
  };

  const handleSearch = (value) => {
    console.log(value);
  };

  useEffect(() => {
    dispatch(fetchDepartmentDetailsApi());
  }, [dispatch]);

  const dataSource = departments.map((department) => {
    return {
      key: department.departmentID,
      name: department.departmentName,
      action: (
        <Flex align="center" gap="small">
          <EyeOutlined
            className="table__icon"
            onClick={() => handleOpenModal(setOpenDetail, department)}
          />
          <EditOutlined
            className="table__icon"
            onClick={() => handleOpenModal(setOpenEditDepartment, department)}
          />
          <Popconfirm
            title="Xoá"
            description="Bạn có muốn xoá phòng ban này?"
            onConfirm={() => handleDelete(department.departmentID)}
            okText="Xoá"
            cancelText="Huỷ"
          >
            <DeleteOutlined className="table__icon" />
          </Popconfirm>
        </Flex>
      ),
    };
  });

  return (
    <>
      <div className="department__list contain">
        <Header
          title="Danh sách phòng ban"
          subTitle="Chi tiết danh sách phòng ban"
        />

        <Card className="department__table table">
          <div className="department__table--head">
            <Flex align="center" justify="space-between">
              <div className="department__search">
                <Form onFinish={handleSearch}>
                  <Form.Item name="search">
                    <Input
                      placeholder="Tìm kiếm"
                      prefix={<SearchOutlined className="table__icon" />}
                      className="table__search"
                    />
                  </Form.Item>
                </Form>
              </div>

              <div className="department__action">
                <Button
                  type="primary"
                  icon={<PlusCircleOutlined />}
                  size="large"
                  onClick={() => setOpenAddDepartment(true)}
                >
                  Thêm phòng ban
                </Button>
              </div>
            </Flex>
          </div>
          <Table
            columns={columns}
            dataSource={dataSource}
            rowSelection={rowSelection}
            style={{ marginTop: 20 }}
          />
        </Card>
      </div>

      {openDetail && (
        <DetailDepartment
          open={openDetail}
          setOpen={setOpenDetail}
          department={department}
        />
      )}
      <AddDepartment open={openAddDepartment} setOpen={setOpenAddDepartment} />
      {openEditDepartment && (
        <EditDepartment
          open={openEditDepartment}
          setOpen={setOpenEditDepartment}
          department={department}
        />
      )}
    </>
  );
};

export default Department;

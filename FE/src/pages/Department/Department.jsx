import Header from "~/components/Header/Header";
import React, { useState } from "react";
import { Button, Card, Flex, Form, Input, Popconfirm, Table, Tag } from "antd";
import {
  EyeOutlined,
  EditOutlined,
  DeleteOutlined,
  SearchOutlined,
  PlusCircleOutlined,
} from "@ant-design/icons";
import { Link } from "react-router-dom";
import DetailDepartment from "~/pages/Department/DetailDepartment";
import "./Department.css";
import AddDepartment from "./AddDepartment";
import { EditDepartment } from "./EditDepartment";

const columns = [
  { title: "Tên phòng ban", dataIndex: "name" },
  { title: "Quản lý", dataIndex: "manager" },
  { title: "Số lượng", dataIndex: "member" },
  { title: "Hành động", dataIndex: "action" },
];

const Department = () => {
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);
  const [openDetail, setOpenDetail] = useState(false);
  const [openAddDepartment, setOpenAddDepartment] = useState(false);
  const [openEditDepartment, setOpenEditDepartment] = useState(false);

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

  const dataSource = Array.from({ length: 8 }).map((_, i) => ({
    key: i,
    name: `Edward King ${i}`,
    manager: `Darlene Robertson ${i}`,
    member: `${i}`,
    action: (
      <Flex align="center" gap="small">
        <EyeOutlined
          className="table__icon"
          onClick={() => setOpenDetail(true)}
        />
        <EditOutlined
          className="table__icon"
          onClick={() => setOpenEditDepartment(true)}
        />
        <Popconfirm
          title="Xoá"
          description="Bạn có muốn xoá phòng ban này?"
          // onConfirm={confirm}
          // onCancel={cancel}
          okText="Yes"
          cancelText="No"
        >
          <DeleteOutlined className="table__icon" />
        </Popconfirm>
      </Flex>
    ),
  }));

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
                  Thêm sadjfnjasfd
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

      <DetailDepartment open={openDetail} setOpen={setOpenDetail} />
      <AddDepartment open={openAddDepartment} setOpen={setOpenAddDepartment} />
      <EditDepartment
        open={openEditDepartment}
        setOpen={setOpenEditDepartment}
      />
    </>
  );
};

export default Department;

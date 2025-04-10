import Header from "~/components/Header/Header";
import React, { useEffect, useState } from "react";
import { Button, Flex, Form, Input, Table, Tag } from "antd";
import {
  EyeOutlined,
  EditOutlined,
  DeleteOutlined,
  SearchOutlined,
  PlusCircleOutlined,
} from "@ant-design/icons";
import "./Employee.css";
import { Link } from "react-router-dom";
import DetailEmployee from "~/pages/Employee/DetailEmployee";

const columns = [
  { title: "Employee Name", dataIndex: "name" },
  { title: "Hire Date", dataIndex: "hireDate" },
  { title: "Department", dataIndex: "department" },
  { title: "Jobs", dataIndex: "job" },
  { title: "Salary", dataIndex: "salary" },
  { title: "Status", dataIndex: "status" },
  { title: "Action", dataIndex: "action" },
];

const ListEmployee = () => {
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);
  const [openDetail, setOpenDetail] = useState(false);

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

  const dataSource = Array.from({ length: 100 }).map((_, i) => ({
    key: i,
    name: `Edward King ${i}`,
    hireDate: "10/10/2025",
    department: `Design`,
    job: `UI/UX Designer`,
    salary: "$3500",
    status: (
      <Tag color="purple" bordered={false}>
        Active
      </Tag>
    ),
    action: (
      <Flex align="center" gap="small">
        <EyeOutlined
          className="table__icon"
          onClick={() => setOpenDetail(true)}
        />
        <EditOutlined className="table__icon" />
        <DeleteOutlined className="table__icon" />
      </Flex>
    ),
  }));

  useEffect(() => {}, []);
  return (
    <>
      <div className="employee__list contain">
        <Header title="All Employees" subTitle="All Employee Information" />

        <div className="employee__table table">
          <div className="employee__table--head">
            <Flex align="center" justify="space-between">
              <div className="employee__search">
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

              <div className="employee__action">
                <Button
                  type="primary"
                  icon={<PlusCircleOutlined />}
                  size="large"
                >
                  <Link to="add">Thêm nhân viên</Link>
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
        </div>
      </div>

      <DetailEmployee open={openDetail} setOpen={setOpenDetail} />
    </>
  );
};

export default ListEmployee;

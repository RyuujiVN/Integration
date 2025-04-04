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
import "./Payroll.css";
import DetailPayroll from "./DetailPayroll";
import EditPayroll from "./EditPayroll";
import AddPayroll from "./AddPayroll";

const columns = [
  { title: "Tên nhân viên", dataIndex: "employee_name" },
  { title: "Lương cứng", dataIndex: "base_salary" },
  { title: "Thưởng", dataIndex: "bonus" },
  { title: "Lương theo tháng", dataIndex: "salary_per_month" },
  { title: "Khấu trừ", dataIndex: "deduction" },
  { title: "Hành động", dataIndex: "action" },
];

const Payroll = () => {
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);
  const [openDetail, setOpenDetail] = useState(false);
  const [openAddPayroll, setOpenAddPayroll] = useState(false);
  const [openEditPayroll, setOpenEditPayroll] = useState(false);

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
    employee_name: `Edward King ${i}`,
    base_salary: `$4500`,
    bonus: `$2000`,
    salary_per_month: `$3000`,
    deduction: `$200`,
    action: (
      <Flex align="center" gap="small">
        <EyeOutlined
          className="table__icon"
          onClick={() => setOpenDetail(true)}
        />
        <EditOutlined
          className="table__icon"
          onClick={() => setOpenEditPayroll(true)}
        />
        <Popconfirm
          title="Xoá nhân viên"
          description="Bạn có chắc muốn xoá nhân viên này?"
          // onConfirm={confirm}
          // onCancel={cancel}
          okText="Xoá"
          cancelText="Huỷ"
        >
          <DeleteOutlined className="table__icon" />
        </Popconfirm>
      </Flex>
    ),
  }));

  return (
    <>
      <div className="payroll__list contain">
        <Header title="Bảng lương" subTitle="Danh sách bảng lương" />

        <Card className="payroll__table table">
          <div className="payroll__table--head">
            <Flex align="center" justify="space-between">
              <div className="payroll__search">
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

              <div className="payroll__action">
                <Button
                  type="primary"
                  icon={<PlusCircleOutlined />}
                  size="large"
                  onClick={() => setOpenAddPayroll(true)}
                >
                  Thêm bảng lương
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

      <DetailPayroll open={openDetail} setOpen={setOpenDetail} />
      <AddPayroll open={openAddPayroll} setOpen={setOpenAddPayroll} />
      <EditPayroll open={openEditPayroll} setOpen={setOpenEditPayroll} />
    </>
  );
};

export default Payroll;

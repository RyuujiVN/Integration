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
import DetailJob from "./DetailJob";
import AddJob from "./AddJob";
import EditJob from "./EditJob";

const columns = [
  { title: "Tên vị trí", dataIndex: "name" },
  { title: "Lương tối thiểu", dataIndex: "minSalary" },
  { title: "Lương tối đa", dataIndex: "maxSalary" },
  { title: "Phòng ban", dataIndex: "department" },
  { title: "Hành động", dataIndex: "action" },
];

const Job = () => {
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);
  const [openDetail, setOpenDetail] = useState(false);
  const [openAddJob, setOpenAddJob] = useState(false);
  const [openEditJob, setOpenEditJob] = useState(false);

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
    name: `UI/UX Desiger`,
    minSalary: `$4000`,
    maxSalary: `$6000`,
    department: (
      <Tag color="success" bordered={false}>
        Design Department
      </Tag>
    ),
    action: (
      <Flex align="center" gap="small">
        <EyeOutlined
          className="table__icon"
          onClick={() => setOpenDetail(true)}
        />
        <EditOutlined
          className="table__icon"
          onClick={() => setOpenEditJob(true)}
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
      <div className="job__list contain">
        <Header title="Vị trí" subTitle="Danh sách vị trí" />

        <Card className="job__table table">
          <div className="job__table--head">
            <Flex align="center" justify="space-between">
              <div className="job__search">
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

              <div className="job__action">
                <Button
                  type="primary"
                  icon={<PlusCircleOutlined />}
                  size="large"
                  onClick={() => setOpenAddJob(true)}
                >
                  Thêm vị trí
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

      <DetailJob open={openDetail} setOpen={setOpenDetail} />
      <AddJob open={openAddJob} setOpen={setOpenAddJob} />
      <EditJob open={openEditJob} setOpen={setOpenEditJob} />
    </>
  );
};

export default Job;

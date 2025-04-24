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
import AddApplicant from "./AddApplicant";
import EditApplicant from "./EditApplicant";
import DetailApplicant from "./DetailApplicant";

const columns = [
  { title: "Tên ứng viên", dataIndex: "name" },
  { title: "Ứng tuyển vị trí", dataIndex: "appliedPosition" },
  { title: "Ngày ứng tuyển", dataIndex: "appliedDate" },
  { title: "Email", dataIndex: "email" },
  { title: "Số điện thoại", dataIndex: "phone" },
  { title: "Trạng thái", dataIndex: "status" },
  { title: "Hành động", dataIndex: "action" },
];

const Applicant = () => {
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);
  const [openDetail, setOpenDetail] = useState(false);
  const [openAddApplicant, setOpenAddApplicant] = useState(false);
  const [openEditApplicant, setOpenEditApplicant] = useState(false);

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
    appliedPosition: `UI/UX Designer`,
    appliedDate: `04/03/2025`,
    email: "kjsdngfjksd@gmail.com",
    phone: "0123456789",
    status: (
      <Tag color="error">Từ chối</Tag>
    ),
    action: (
      <Flex align="center" gap="small">
        <EyeOutlined
          className="table__icon"
          onClick={() => setOpenDetail(true)}
        />
        <EditOutlined
          className="table__icon"
          onClick={() => setOpenEditApplicant(true)}
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
      <div className="applicant__list contain">
        <Header
          title="Ứng viên"
          subTitle="Danh sách ứng viên"
        />

        <Card className="applicant__table table">
          <div className="applicant__table--head">
            <Flex align="center" justify="space-between">
              <div className="applicant__search">
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

              <div className="applicant__action">
                <Button
                  type="primary"
                  icon={<PlusCircleOutlined />}
                  size="large"
                  onClick={() => setOpenAddApplicant(true)}
                >
                  Thêm ứng viên
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

      <DetailApplicant open={openDetail} setOpen={setOpenDetail} />
      <AddApplicant open={openAddApplicant} setOpen={setOpenAddApplicant} />
      <EditApplicant open={openEditApplicant} setOpen={setOpenEditApplicant} />
    </>
  );
};

export default Applicant;

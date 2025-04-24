import { useEffect, useState } from "react";
import { Button, Card, Flex, Form, Input, Popconfirm, Table, Tag } from "antd";
import Header from "~/components/Header/Header";
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
  { title: "Tên nhân viên", dataIndex: "name" },
  { title: "Ngày vào làm", dataIndex: "hireDate" },
  { title: "Phòng ban", dataIndex: "department" },
  { title: "Vị trí", dataIndex: "job" },
  { title: "Lương", dataIndex: "salary" },
  { title: "Trạng thái", dataIndex: "status" },
  { title: "Hành động", dataIndex: "action" },
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
        <Link to="edit">
          <EditOutlined className="table__icon" />
        </Link>
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

  useEffect(() => {}, []);
  return (
    <>
      <div className="employee__list contain">
        <Header title="Nhân viên" subTitle="Danh sách nhân viên" />

        <Card className="employee__table table">
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
        </Card>
      </div>

      <DetailEmployee open={openDetail} setOpen={setOpenDetail} />
    </>
  );
};

export default ListEmployee;

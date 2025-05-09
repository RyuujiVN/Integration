import Header from "~/components/Header/Header";
import React, { useEffect, useState } from "react";
import { Button, Card, Flex, Form, Input, Popconfirm, Table, Tag } from "antd";
import {
  EyeOutlined,
  EditOutlined,
  DeleteOutlined,
  SearchOutlined,
  PlusCircleOutlined,
} from "@ant-design/icons";
import { Link } from "react-router-dom";
import DetailPosition from "./DetailPosition";
import AddPosition from "./AddPosition";
import EditPosition from "./EditPosition";
import { useDispatch, useSelector } from "react-redux";
import {
  deletePosition,
  deletePositionApi,
  fetchPositionAllApi,
} from "~/redux/position/positionSlice";
import { toast } from "react-toastify";

const columns = [
  { title: "Mã vị trí", dataIndex: "positionID" },
  { title: "Tên vị trí", dataIndex: "positionName" },
  { title: "Hành động", dataIndex: "action" },
];

const Position = () => {
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);
  const [openDetail, setOpenDetail] = useState(false);
  const [openAddPosition, setOpenAddPosition] = useState(false);
  const [openEditPosition, setOpenEditPosition] = useState(false);
  const [position, setPosition] = useState(null);

  const positions = useSelector((state) => state.position.currentPosition);

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchPositionAllApi());
  }, [dispatch]);

  const onSelectChange = (newSelectedRowKeys) => {
    console.log("selectedRowKeys changed: ", newSelectedRowKeys);
    setSelectedRowKeys(newSelectedRowKeys);
  };
  const rowSelection = {
    selectedRowKeys,
    onChange: onSelectChange,
  };

  const handleOpenModal = (setOpen, position) => {
    setOpen(true);
    setPosition(position);
  };

  const handleDelete = (positionID) => {
    toast.promise(dispatch(deletePositionApi(positionID)), {
      pending: "Đang xoá...",
      success: "Xoá vị trí thành công!",
    });
    dispatch(deletePosition(positionID));
  };

  const handleSearch = (value) => {
    console.log(value);
  };

  const dataSource = positions.map((position) => {
    return {
      key: position.positionID,
      positionID: position.positionID,
      positionName: position.positionName,
      action: (
        <Flex align="center" gap="small">
          <EyeOutlined
            className="table__icon"
            onClick={() => handleOpenModal(setOpenDetail, position)}
          />
          <EditOutlined
            className="table__icon"
            onClick={() => handleOpenModal(setOpenEditPosition, position)}
          />
          <Popconfirm
            title="Xoá vị trí"
            description="Bạn có chắc muốn xoá vị trí này?"
            onConfirm={() => handleDelete(position.positionID)}
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
      <div className="position__list contain">
        <Header title="Vị trí" subTitle="Danh sách vị trí" />

        <Card className="position__table table">
          <div className="position__table--head">
            <Flex align="center" justify="space-between">
              <div className="position__search">
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

              <div className="position__action">
                <Button
                  type="primary"
                  icon={<PlusCircleOutlined />}
                  size="large"
                  onClick={() => setOpenAddPosition(true)}
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

      {openDetail && (
        <DetailPosition
          open={openDetail}
          setOpen={setOpenDetail}
          position={position}
        />
      )}
      {openAddPosition && (
        <AddPosition open={openAddPosition} setOpen={setOpenAddPosition} />
      )}
      {openEditPosition && (
        <EditPosition
          open={openEditPosition}
          setOpen={setOpenEditPosition}
          position={position}
        />
      )}
    </>
  );
};

export default Position;

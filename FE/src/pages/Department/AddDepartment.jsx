import { Button, Form, Input, Modal } from "antd";
import React from "react";
import { fetchDepartmentAddApi } from "~/redux/department/departmentSlice";
import { useDispatch } from "react-redux";
import { toast } from "react-toastify";

const AddDepartment = (props) => {
  const { open, setOpen } = props;
  const dispatch = useDispatch();
  const [form] = Form.useForm();

  const handleAdd = async (value) => {
    toast.promise(dispatch(fetchDepartmentAddApi(value)), {
      pending: "Đang thêm...",
    });
  };

  return (
    <>
      <Modal
        title={<h4 className="modal__title">Thêm phòng ban</h4>}
        open={open}
        onCancel={() => setOpen(false)}
        footer={[
          <Button key="cancel" onClick={() => setOpen(false)}>
            Huỷ
          </Button>,
          <Button
            key="submit"
            type="primary"
            htmlType="submit"
            onClick={() => form.submit()}
          >
            Thêm phòng ban
          </Button>,
        ]}
      >
        <Form form={form} onFinish={handleAdd} layout="vertical">
          <Form.Item
            name="departmentID"
            label="Id phòng ban"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập tên phòng ban",
              },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            name="departmentName"
            label="Tên phòng ban"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập tên phòng ban",
              },
            ]}
          >
            <Input />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
};

export default AddDepartment;

import { Button, Form, Input, Modal } from "antd";
import { useDispatch } from "react-redux";
import { toast } from "react-toastify";
import { addPositionApi } from "~/redux/position/positionSlice";

const AddPostion = (props) => {
  const { open, setOpen } = props;
  const dispatch = useDispatch();
  const [form] = Form.useForm();

  const handleAdd = (value) => {
    toast.promise(dispatch(addPositionApi(value)), {
      pending: "Đang thêm vị trí...",
    });
  };

  return (
    <>
      <Modal
        title={<h4 className="modal__title">Thêm vị trí</h4>}
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
            Thêm
          </Button>,
        ]}
      >
        <Form form={form} onFinish={handleAdd} layout="vertical">
          <Form.Item
            name="positionID"
            label="Mã vị trí"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập mã vị trí",
              },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            name="positionName"
            label="Tên vị trí"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập tên vị trí",
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

export default AddPostion;

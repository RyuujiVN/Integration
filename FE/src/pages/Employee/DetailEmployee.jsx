import { Modal } from "antd";

const DetailEmployee = (props) => {
  const { open, setOpen } = props;

  return (
    <>
      <Modal
        title={<h4 className="modal__title">Thông tin chi tiết</h4>}
        open={open}
        footer={null}
        onCancel={() => setOpen(false)}
      >
        <p>
          <strong>Tên:</strong>
        </p>
        <p>
          <strong>Ngày vào làm:</strong>
        </p>
        <p>
          <strong>Phòng ban:</strong>
        </p>
      </Modal>
    </>
  );
};

export default DetailEmployee;

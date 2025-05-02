import { Modal } from "antd";
import React from "react";

const DetailApplicant = (props) => {
  const { open, setOpen } = props;

  return (
    <>
      <Modal
        title={<h4 className="modal__title">Chi tiết phòng ban</h4>}
        open={open}
        onCancel={() => setOpen(false)}
        footer={null}
      >
        <p>
          <strong>Tên phòng ban:</strong>
        </p>
        <p>
          <strong>Mô tả:</strong>
        </p>
      </Modal>
    </>
  );
};

export default DetailApplicant;

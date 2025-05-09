const messageUtil = (messageApi, type, content) => {
  messageApi.open({
    type: type,
    content: content,
  });
};


export default messageUtil;

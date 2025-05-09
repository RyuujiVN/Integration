import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import { toast } from 'react-toastify'
import instance from '~/api/intance'

const initialState = {
  currentPosition: [],
}

export const fetchPositionAllApi = createAsyncThunk(
  "position/fetchPositionAllApi",
  async () => {
    const response = await instance.get("/Position/getall")

    return response.data
  }
)

export const addPositionApi = createAsyncThunk(
  "position/addPositionApi",
  async (data) => {
    const response = await instance.post("/Position/AddPosition", data)

    return response.data
  }
)

export const editPositionApi = createAsyncThunk(
  "position/editPositionApi",
  async ({ id, data }) => {
    const response = await instance.put(`/Position/update/${id}`, data)

    return response.data
  }
)

export const deletePositionApi = createAsyncThunk(
  "position/deletePositionApi",
  async (id) => {
    const response = await instance.delete(`/Position/delete/${id}`)

    return response.data
  }
)

export const positionSlice = createSlice({
  name: 'position',
  initialState,
  reducers: {
    deletePosition: (state, action) => {
      state.currentPosition = state.currentPosition.filter(position => position.positionID !== action.payload)
    }
  },

  extraReducers: (builder) => {
    builder.addCase(fetchPositionAllApi.fulfilled, (state, action) => {
      state.currentPosition = action.payload
    })

    builder.addCase(addPositionApi.fulfilled, (state, action) => {
      state.currentPosition.push(action.payload)
      toast.success("Thêm vị trí thành công!")
    })

    builder.addCase(editPositionApi.fulfilled, (state, action) => {
      const index = state.currentPosition.findIndex(position => position.positionID === action.payload.positionID)

      state.currentPosition[index] = action.payload
      toast.success("Cập nhật vị trí thành công!")
    })
  }
})

// Action creators are generated for each case reducer function
export const { deletePosition } = positionSlice.actions

export const positionReducer = positionSlice.reducer
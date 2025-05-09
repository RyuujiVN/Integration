import { configureStore } from '@reduxjs/toolkit'
import { departmentReducer } from "./department/departmentSlice"
import { payrollReducer } from "./payroll/payrollSlice"
import { positionReducer } from "./position/positionSlice"

export const store = configureStore({
  reducer: {
    department: departmentReducer,
    payroll: payrollReducer,
    position: positionReducer
  }
})
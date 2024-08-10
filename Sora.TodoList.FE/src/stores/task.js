import { defineStore } from "pinia";
import { reactive, ref } from "vue";
import ConstAPI from "@/apis/const/ConstAPI";
import commonConst from "@/commons/commonConst";
import TaskAPI from "@/apis/task/TaskAPI";
import CommonFunction from "@/commons/CommonFunction";

export const useTaskStore = defineStore("task", () => {
    const statusDic = ref([]);

    const filter = reactive({
        statusList: [],
        skip: 0,
        limit: 20,
        rangeDate: [
            CommonFunction.initDate(new Date()).startOf("month").toDate(),
            CommonFunction.initDate(new Date()).endOf("month").toDate(),
        ],
    });

    const tasks = ref([]);

    // lấy danh sách trạng thái công việc
    async function getListTaskStatus() {
        const result = await ConstAPI.getListConst(
            commonConst.ConstDic.TaskStatus
        );

        statusDic.value = result.data ?? [];
    }

    async function getListTasks() {
        const result = await TaskAPI.getList({
            skip: filter.skip,
            limit: filter.limit,
            statusList: filter.statusList.map((x) => x.key),
            fromDate: filter.rangeDate[0]
                ? CommonFunction.initDate(filter.rangeDate[0]).format(
                      "YYYY-MM-DDTHH:mm:ssZ"
                  )
                : null,
            toDate: filter.rangeDate[1]
                ? CommonFunction.initDate(filter.toDate).format(
                      "YYYY-MM-DDTHH:mm:ssZ"
                  )
                : null,
        });

        tasks.value = result.data.items;
    }

    return { statusDic, filter, tasks, getListTaskStatus, getListTasks };
});

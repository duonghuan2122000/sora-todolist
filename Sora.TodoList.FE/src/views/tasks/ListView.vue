<!-- 
    Trang danh sách task
-->
<template>
    <div class="TasksListView">
        <div class="TitleBox">
            <div class="Title">Danh sách công việc</div>
            <div class="ButtonBox">
                <Button
                    type="button"
                    label="Thêm"
                    @click="router.push({ name: 'CreateTask' })"
                />
            </div>
        </div>
        <div class="FilterBox">
            <div>
                <label for="StatusList" class="StatusListLabel"
                    >Trạng thái</label
                >

                <MultiSelect
                    v-model="TaskStore.filter.statusList"
                    :options="TaskStore.statusDic"
                    optionLabel="value"
                    filter
                    fluid
                    placeholder="Trạng thái"
                    :maxSelectedLabels="2"
                />
            </div>
            <div class="FilterItem">
                <label for="RangeDateFilter" class="RangeDateFilterLabel"
                    >Thời gian</label
                >
                <DatePicker
                    fluid
                    v-model="TaskStore.filter.rangeDate"
                    dateFormat="dd/mm/yy"
                    selectionMode="range"
                    :manualInput="false"
                    showButtonBar
                />
            </div>
            <div class="FilterItem">
                <Button
                    type="button"
                    label="Lọc"
                    @click="TaskStore.getListTasks"
                />
            </div>
        </div>

        <div class="DataBox">
            <DataTable
                showGridlines
                paginator
                :rows="5"
                :rowsPerPageOptions="[5, 10, 20, 50]"
                paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
                currentPageReportTemplate="{first} đến {last} trong {totalRecords}"
                class="DataTable"
                :value="TaskStore.tasks"
                tableStyle="min-width: 50rem;"
            >
                <Column field="title" header="Tiêu đề"></Column>
                <Column header="Trạng thái">
                    <template #body="{ data }">
                        <Badge
                            size="large"
                            :value="data.statusText"
                            :severity="
                                data.extraProperties?.level ?? 'secondary'
                            "
                        />
                    </template>
                </Column>
                <Column header="Người tạo">
                    <template #body="{ data }">
                        {{ data.userCreated?.email }}
                    </template>
                </Column>
                <Column header="Người được giao">
                    <template #body="{ data }">
                        {{ data.userAssign?.email }}
                    </template>
                </Column>
                <Column>
                    <template #body>
                        <Button
                            severity="secondary"
                            icon="pi pi-file-edit"
                            label="Cập nhật"
                        />
                        <Button
                            style="margin-left: 8px"
                            severity="secondary"
                            icon="pi pi-delete-left"
                            label="Xóa"
                        />
                    </template>
                </Column>
            </DataTable>
        </div>
    </div>
</template>

<script setup>
import { useTaskStore } from "@/stores/task";
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";

const TaskStore = useTaskStore();
const router = useRouter();

onMounted(() => {
    TaskStore.getListTaskStatus();
    setTimeout(() => {
        TaskStore.getListTasks();
    }, 1000);
});
</script>

<style lang="scss" scoped>
.TasksListView {
    height: 100%;
    overflow: hidden;
    width: 80%;
    margin: 0 auto;
    padding: 16px 0;
    display: flex;
    flex-direction: column;

    .TitleBox {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
        flex: 0;
        padding-bottom: 16px;

        .Title {
            font-weight: 600;
            font-size: 20px;
        }
    }

    .FilterBox {
        border-top: 1px solid #ccc;
        padding-top: 16px;
        flex: 0;
        display: flex;
        flex-direction: row;
        align-items: flex-end;

        .StatusListLabel,
        .RangeDateFilterLabel {
            margin-bottom: 4px;
            display: block;
        }

        .FilterItem {
            margin-left: 8px;
        }
    }

    .DataBox {
        flex: 1;
        padding-top: 16px;
    }
}
</style>

<style lang="scss">
.TasksListView {
    .DataTable {
        height: 100%;
        display: flex;
        flex-direction: column;
    }
    .p-datatable-table-container {
        flex: 1;
    }
}
</style>

<!-- 
    Tạo công việc
-->
<template>
    <div class="CreateTaskView">
        <div class="TitleBox">
            <div class="Title">Thêm công việc</div>
        </div>
        <hr />
        <div class="InputGroup">
            <label for="TitleField">Tiêu đề</label>
            <InputText id="TitleField" autofocus />
        </div>
        <div class="InputGroup">
            <label for="ContentField">Nội dung</label>
            <Editor editorStyle="height: 320px" />
        </div>
        <div class="SubmitBox">
            <Button type="button" label="Tạo" />
            <Button type="button" severity="secondary" label="Lưu nháp" />
        </div>
    </div>
</template>

<style lang="scss" scoped>
.CreateTaskView {
    height: 100%;
    width: 80%;
    margin: 0 auto;
    padding: 16px 0;

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

    .InputGroup {
        display: flex;
        flex-direction: column;
        gap: 8px;
        margin-top: 16px;
    }

    .SubmitBox {
        display: flex;
        flex-direction: row;
        align-items: center;
        margin-top: 16px;
        gap: 8px;
    }
}
</style>

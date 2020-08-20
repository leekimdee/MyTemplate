$(document).ready(function () {
    $('#PhongBanForm').validate({
        errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page  
        errorElement: 'div',
        errorPlacement: function (error, e) {
            e.parents('.form-group > div').append(error);
        },
        highlight: function (e) {

            $(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
            $(e).closest('.help-block').remove();
        },
        success: function (e) {
            e.closest('.form-group').removeClass('has-success has-error');
            e.closest('.help-block').remove();
        },
        rules: {
            //'Email': {
            //    required: true,
            //    email: true
            //},

            'tenphong': {
                required: true
            },

            'thutusapxep': {
                number: true
            }
        },
        messages: {
            //'Email': 'Please enter valid email address',

            'tenphong': {
                required: 'Tên phòng không được để trống',
            },

            'thutusapxep': {
                number: 'Chỉ nhập số'
            }
        },
        submitHandler: function () {
            if (phongbanId != 0) {
                Update();
            }
            else {
                AddNew();
            }
            return false;
        }
    });
});
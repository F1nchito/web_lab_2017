const Ajv = require('ajv');
const ajv = Ajv({
    allErrors: true,
    removeAdditional: 'all'
});
const productSchema = require('./product_schema.js');
ajv.addSchema(productSchema, 'new-product');

function errorResponce(schemaErrors) {
    let errors = schemaErrors.map((error) => {
        var target, msg;
        switch (error.keyword) {
            case "format":
                target = error.params.format;
                msg = error.message;
                break;
            case "pattern":
                target = error.dataPath.slice(1);
                msg = 'Field must be non-empty and not contain white spaces';
                break;
            case "exclusiveMinimum":
                target = error.dataPath.slice(1);
                msg = error.message;
                break;
            case "required":
                target = error.params.missingProperty;
                msg = error.message;
                break;
            case "type":
                target = error.dataPath.slice(1);
                msg = error.message;
                break;
            case "maxLength":
                target = error.dataPath;
                target = target.slice(1);
                msg = error.message;
                break;
            default:

                break;
        }
        return {
            target: target,
            msg: msg
        }
    });
    return errors;
};
module.exports = (schemaName) => {
    return (req, res, next) => {
        let valid = ajv.validate(schemaName, req.body);
        if (!valid) {
            return res.status(400).send(errorResponce(ajv.errors));
        }
        next();
    }
};
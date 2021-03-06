module.exports = {
    'title': 'new product',
    'description': 'Properties required to create a product',
    'type': 'object',
    'properties': {
        'name': {
            'type': 'string',
            'pattern': '^\\S+$',
            'maxLength': 15,
            'description': 'productname'
        },
        'email': {
            'type': 'string',
            'format': 'email',
            'description': 'email'
        },
        'count': {
            'type': 'integer'
        },
        'price': {
            'type': 'number',
            'exclusiveMinimum': 0
        },
        'delivery':{
            'type':'string'
        },
        'country': {
            'type':'string'
        },
        'city': {
            'type':'array',
            'items':{'type':'string'}
        }
    },
    'required': ['name', 'email', 'count', 'price','delivery']
};
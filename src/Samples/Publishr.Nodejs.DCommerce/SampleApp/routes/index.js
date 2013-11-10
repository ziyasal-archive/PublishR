
/*
 * GET home page.
 */
var products = [
{id:1, name:"Puma Shoes" },
{id:2, name:"Nike Shoes"},
{id:3, name:"Converse Wallet"}
];

exports.index = function(req, res){
  res.render('index', { title: 'Express',products: products});
};


string query = new SqlSelectQueryBuilder()
                .Select("column1")
                .Select("column2")
                .From("tableName")
                .Where("condition1", "value1")
                .Where("condition2", "value2")
                .Build();
Console.WriteLine($"Query : {query}");
public class SelectQueryBuilder

{
    private string _selectClause;
    private string _fromClause;
    private string _whereClause;
    private string _orderByClause;

    public SelectQueryBuilder Select(string select)
    {
        _selectClause = select;
        return this;
    }

    public SelectQueryBuilder From(string from)
    {
        _fromClause = from;
        return this;
    }

    public SelectQueryBuilder Where(string where)
    {
        _whereClause = where;
        return this;
    }

    public SelectQueryBuilder OrderBy(string orderBy)
    {
        _orderByClause = orderBy;
        return this;
    }

    public string Build()
    {
        var query = $"SELECT {_selectClause} " +
                    $"FROM {_fromClause}";

        if (!string.IsNullOrEmpty(_whereClause))
        {
            query += $" WHERE {_whereClause}";
        }

        if (!string.IsNullOrEmpty(_orderByClause))
        {
            query += $" ORDER BY {_orderByClause}";
        }

        return query;
    }


}




public class SqlSelectQueryBuilder
{
    private List<string> selectColumns;
    private string tableName;
    private string whereClause;
   // private Dictionary<int,object> parameters;
    private int valuecount = 0;
    public SqlSelectQueryBuilder()
    {
        selectColumns = new List<string>();
       // parameters = new Dictionary<int, object>();
    }

    public SqlSelectQueryBuilder Select(string column)
    {
        selectColumns.Add(column);
        return this;
    }

    public SqlSelectQueryBuilder From(string table)
    {
        tableName = table;
        return this;
    }

    public SqlSelectQueryBuilder Where(string condition, object value)
    {
        if (string.IsNullOrEmpty(whereClause))
        {
            whereClause = $"WHERE {condition} = @param{valuecount}";
        }
        else
        {
            whereClause += $" AND {condition} = @param{valuecount}";
        }
        valuecount++;
       // parameters.Add(parameters.Count, value);
        return this;
    }

    public string Build()
    {
        string query = $"SELECT {string.Join(", ", selectColumns)} FROM {tableName} {whereClause}";
        return query;
    }
}


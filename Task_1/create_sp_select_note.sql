CREATE or replace FUNCTION select_note(_id uuid) returns
    TABLE(id uuid  ,header varchar(128),body varchar(1024),
    is_deleted boolean , modified_at timestamptz,
    user_id int, first_name varchar(128), last_name varchar(128))
AS
    $$
    begin
        return QUERY
            SELECT notes.id,notes.header,notes.body,
                   notes.is_deleted,notes.modified_at,
                   notes.user_id,u.first_name,u.last_name
            from notes left join users u on u.id = notes.user_id
            where notes.id = $1;
    end
    $$
LANGUAGE plpgsql;
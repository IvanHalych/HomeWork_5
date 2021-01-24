CREATE or replace FUNCTION insert_note(_id uuid, _header varchar(128),
_body varchar(1024),_user_id int) returns
    TABLE(id uuid  ,header varchar(128),body varchar(1024),user_id int)
AS
    $$
    begin
        INSERT into notes(id, header, body, user_id, is_deleted)  VALUES ($1,$2,$3,$4,false);
        return QUERY
            SELECT notes.id,notes.header,notes.body,notes.user_id
            from notes
            where notes.modified_at = (select max(notes.modified_at) from notes);
    end
    $$
LANGUAGE plpgsql;
package com.flyerme.weautotools.mapper;

import com.flyerme.weautotools.entity.ClickCounter;
import org.apache.ibatis.annotations.*;

import java.time.LocalDateTime;
import java.util.List;

/**
 * 点击计数器Mapper接口
 *
 * @author WeAutoTools Team
 * @version 1.0.0
 * @since 2025-08-15
 */
@Mapper
public interface ClickCounterMapper {

    /**
     * 根据ID查询计数器
     */
    @Select("SELECT * FROM click_counter WHERE id = #{id} AND deleted = 0")
    ClickCounter selectById(Long id);

    /**
     * 根据名称查询计数器
     */
    @Select("SELECT * FROM click_counter WHERE counter_name = #{counterName} AND deleted = 0")
    ClickCounter selectByName(String counterName);

    /**
     * 查询所有计数器
     */
    @Select("SELECT * FROM click_counter WHERE deleted = 0 ORDER BY created_at DESC")
    List<ClickCounter> selectAll();

    /**
     * 查询启用的计数器
     */
    @Select("SELECT * FROM click_counter WHERE enabled = true AND deleted = 0 ORDER BY created_at DESC")
    List<ClickCounter> selectEnabled();

    /**
     * 插入计数器
     */
    @Insert("INSERT INTO click_counter (counter_name, description, click_count, enabled, created_at, updated_at, deleted, version) " +
            "VALUES (#{counterName}, #{description}, #{clickCount}, #{enabled}, #{createdAt}, #{updatedAt}, #{deleted}, #{version})")
    @Options(useGeneratedKeys = true, keyProperty = "id")
    int insert(ClickCounter clickCounter);

    /**
     * 更新计数器
     */
    @Update("UPDATE click_counter SET counter_name = #{counterName}, description = #{description}, " +
            "enabled = #{enabled}, updated_at = #{updatedAt}, version = version + 1 " +
            "WHERE id = #{id} AND deleted = 0 AND version = #{version}")
    int update(ClickCounter clickCounter);

    /**
     * 增加点击次数
     */
    @Update("UPDATE click_counter SET click_count = click_count + 1, last_click_time = #{lastClickTime}, " +
            "updated_at = #{updatedAt}, version = version + 1 " +
            "WHERE id = #{id} AND enabled = true AND deleted = 0")
    int incrementClickCount(@Param("id") Long id, 
                           @Param("lastClickTime") LocalDateTime lastClickTime,
                           @Param("updatedAt") LocalDateTime updatedAt);

    /**
     * 根据名称增加点击次数
     */
    @Update("UPDATE click_counter SET click_count = click_count + 1, last_click_time = #{lastClickTime}, " +
            "updated_at = #{updatedAt}, version = version + 1 " +
            "WHERE counter_name = #{counterName} AND enabled = true AND deleted = 0")
    int incrementClickCountByName(@Param("counterName") String counterName,
                                 @Param("lastClickTime") LocalDateTime lastClickTime,
                                 @Param("updatedAt") LocalDateTime updatedAt);

    /**
     * 逻辑删除计数器
     */
    @Update("UPDATE click_counter SET deleted = 1, updated_at = #{updatedAt} WHERE id = #{id} AND deleted = 0")
    int deleteById(@Param("id") Long id, @Param("updatedAt") LocalDateTime updatedAt);

    /**
     * 统计计数器总数
     */
    @Select("SELECT COUNT(*) FROM click_counter WHERE deleted = 0")
    long count();

    /**
     * 统计启用的计数器总数
     */
    @Select("SELECT COUNT(*) FROM click_counter WHERE enabled = true AND deleted = 0")
    long countEnabled();
}
